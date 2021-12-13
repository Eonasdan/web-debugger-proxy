const fs = require('fs');
const path = require('path');

/***
 * Expects a list of { source: PATH, destination: PATH, name: FIlE-NAME }.
 * If destination is not provided, wwwroot + filename will be used.
 */
function copy() {
    [
        {
            source: './node_modules/bootstrap/dist/js/bootstrap.bundle.min.js',
            destination: './wwwroot/js/bootstrap.bundle.min.js',
        },
        {
            source: './node_modules/bootstrap/dist/css/bootstrap.min.css',
            destination: './wwwroot/css/bootstrap.min.css',
        },
        {
            source: './node_modules/@fortawesome/fontawesome-free/js/fontawesome.min.js',
            destination: './wwwroot/js/fontawesome.min.js',
        },
        {
            source: './node_modules/@fortawesome/fontawesome-free/js/solid.min.js',
            destination: './wwwroot/js/solid.min.js',
        },
    ].forEach(file => {
        const destination = file.destination || `./wwwroot/${file.name}`;
        console.log(`copying ${file.source} to ${destination}`);
        fs.mkdirSync(path.dirname(file.destination), { recursive: true });
        fs.copyFileSync(file.source, destination)
    });
}

copy();