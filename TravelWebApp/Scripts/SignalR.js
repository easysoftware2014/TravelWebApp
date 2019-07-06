
(function () {
    var myHub = $.connection.travelChatHub;

    $.connection.hub.start()
        .done(function () {
            writeToPage("It worked");
            myHub.server.globalBroadCast("connected");
            myHub.server.getServerDateTime()
                .done(function(data) {
                    writeToPage(data);
                })
                .fail(function(e) {
                    writeToPage(e);
                });
        })
        .fail(function () { writeToPage("Error connecting to SignalR"); });

    myHub.client.globalBroadCast = function (message) {
        writeToPage(message);
    };

    function writeToPage(message) {
        $("#signalR_message").append(message + "<br/>");
    }

})();

