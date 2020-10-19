"use strict";
var page = require('webpage').create(),
    system = require('system'),
    address,
    output;

console.log('Usage: rasterize.js [URL] [filename] [paperformat] [orientation]');
address = system.args[1];
output = system.args[2];    

page.viewportSize = { width: 800, height: 600 };

page.paperSize = {
    format: system.args[3],
    orientation: system.args[4],
    margin: '0.5cm',
    footer: {
        height: "0.5cm",
        contents: phantom.callback(function (pageNum, numPages) {
            return "<span style='float:right; font-family: sans-serif; font-size: 10px; font-weight: 400;'>" + pageNum + " / " + numPages + "</span>";
        })
    }
};

page.open(address, function (status) {
    if (status !== 'success') {
        console.log('Unable to load the address!');
        phantom.exit(1);
    } else {
        window.setTimeout(function () {
            page.render(output);
            phantom.exit();
        }, 200);
    }
});
