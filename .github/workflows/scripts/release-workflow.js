const { execSync, execFile, spawn } = require('node:child_process');
const { readFile, readdir } = require('node:fs/promises');
const { resolve, extname, basename } = require('node:path');
const { promisify } = require('node:util');

const execFileAsync = promisify(execFile);

/**
 * Get latest Git tag.
 * @param {Object} [options]
 * @param {string}  [options.prefix]
 * @returns {Promise<string|null>}
 */
async function latestTag (options = {}) {
    if (!options.prefix) throw new Error("Prefix is required.");

    // const cmd = `git tag --merged HEAD --list '${options.prefix}@*'  --sort=-v:refname`;
    // const cmdList = `git tag -l`
    //
    // const outList = execSync(cmdList, {encoding: 'utf8'}).trim().split('\n');
    //
    // console.log('🏷️ Found git tags:', outList)

    const cmd = `git describe --tags --match="${options.prefix}@*" --abbrev=0`

    const out = execSync(cmd, {encoding: 'utf8'}).trim().split('\n');

    if (!out[0]) throw new Error(`No tags matching ${options.prefix}`);


    // console.log(`✅ ${options.prefix} - found git tags:\n ${out}`);

    // return latest SemVer
    return out[0];
}


async function readLatestReleaseVersion(csProjectFilePath) {
    const stringXml = await readFile(resolve(csProjectFilePath), 'utf8');

    // console.log(stringXml);

    const match = stringXml.match(/<ReleaseVersion>\s*([^<]+)\s*<\/ReleaseVersion>/);

    // console.log('match:', match[1]);


    if (!match) throw new Error(`ReleaseVersion not found in ${csProjectFilePath}`);

    return match[1].trim();

}

const getVersionFromIncomingTag = (tag) => {
    return tag.replace(/^[^@]*@/, '')
}

async function validateIncomingPackageVersion(latestGitTag, versionFromLatestRelease) {
    // Strip prefix with @
    const versionFromIncomingGitTag = getVersionFromIncomingTag(latestGitTag);

    // Ensure both are exactly '1.2.3.4' format
    const dotted = /^\d+\.\d+\.\d+\.\d+$/;
    if (!dotted.test(versionFromIncomingGitTag) ||
        !dotted.test(versionFromLatestRelease)
    ) {
        throw new Error('Versions must follow x.major.minor.patch (four numeric numbers)');
    }

    // Ensure version is not regressing
    const tag = versionFromIncomingGitTag.split('.').map(Number);
    const file = versionFromLatestRelease.split('.').map(Number);


    // Ensure incoming tag > latest release
    for (let i = 0; i < 4; i++) {
        if (tag[i] > file[i]) return;    // ✅ tag is newer → succeed
        if (tag[i] < file[i]) {          // ❌ tag is older → fail
            throw new Error(
                `Incoming tag (${versionFromIncomingGitTag}) must be higher than latest release (${versionFromLatestRelease})`
            );
        }
    }

    throw new Error(`Incoming tag (${versionFromIncomingGitTag}) is identical to latest release (${versionFromLatestRelease}). Skipping.`);
}

/**
 * Recursively collect every file whose name matches `matcher(entryName)`.
 * Returns an array of absolute paths.
 */
async function collectFiles(dir, matcher, acc = []) {
    for (const entry of await readdir(dir, { withFileTypes: true })) {
        const full = resolve(dir, entry.name);
        if (entry.isDirectory()) {
            await collectFiles(full, matcher, acc);
        } else if (matcher(entry.name)) {
            acc.push(full);
        }
    }
    return acc;
}

/** Convenience wrapper for *.csproj */
async function findCsprojFiles(root = process.cwd()) {
    return collectFiles(root, name => extname(name).toLowerCase() === '.csproj');
}

function getTagPrefix(csprojPath) {
    const file = basename(csprojPath, extname(csprojPath));   // "Otm.Model"
    const match = file.match(/^Otm\.(.+)$/i);        // capture after "Otm."
    return match ? match[1] : file;                           // fallback if pattern unknown
}



// Wrapper to call release script
async function runReleaseScript(csprojPath, versionToRelease) {
    const shellScript = resolve(__dirname, './release.sh')

    console.log(`Running: ${shellScript} ${csprojPath} ${versionToRelease}`);

    try {
        const { stdout, stderr } = await execFileAsync(shellScript, [csprojPath, versionToRelease]);
        console.log('STDOUT:', stdout);
        console.log('STDERR:', stderr);
    } catch (error) {
        console.error('Script failed:', error);
        throw error;
    }
}


/* CLI */
/* node queryTag.js Profile */
if (require.main === module) {
    (async () => {
        const [, , ] = process.argv;

        try {
            const csprojectsToProcess = await findCsprojFiles();

            console.log('Found .csproj files:\n', csprojectsToProcess.join('\n'), '\n');

            // Run sequentially using for...of loop
            for (const csproj of csprojectsToProcess) {
                try {
                    const tagPrefix = getTagPrefix(csproj);

                    const incomingTag = await latestTag({ prefix: tagPrefix });
                    const incomingVersion = getVersionFromIncomingTag(incomingTag);

                    const latestVersion = await readLatestReleaseVersion(csproj);

                    console.log('\n');
                    console.log(`Project ${tagPrefix} - found git tag:          ${incomingTag}`);

                    // console.log(`Project ${tagPrefix} - found release version:  ${latestVersion}`);
                    // await validateIncomingPackageVersion(incomingTag, latestVersion);
                    // console.log(`Project ${tagPrefix} - ✅ New incoming git tag is valid and not regressing. Triggering release to NuGet Gallery command...\n`);

                    console.log(`Project ${tagPrefix} - calling release script with args: ${csproj} ${incomingVersion}`);

                    await runReleaseScript(csproj, incomingVersion); // throws if publish fails

                    // done!

                    // await persistReleasedVersion(csproj, incomingVersion);


                } catch (err) {
                    console.error(`❌ ${csproj}`, err.message);
                }
            }


        } catch (err) {
            console.error('❌', err.message)
            process.exit(1);
        }
    })();

}
