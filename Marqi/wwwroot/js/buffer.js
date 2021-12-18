"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/v1/bufferhub").build();

connection.on("BufferChanged", function (user, message) {
    document.getElementById("buffer").setAttribute("src", "/v1/buffer?=" + Date.now());
});

connection.start().then(function () {}).catch(function (err) {
    return console.error(err.toString());
});