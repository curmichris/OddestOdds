"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/messages").build();

connection.on("UpdatedOdds", function (odds) {
    var ulElement = document.getElementById("allOdds");
    ulElement.innerHTML = "";
    odds.forEach(function (odd) {
        var li = document.createElement("li");
        li.innerHTML =
            "<h3><em><img src=\"/images/img02.jpg\" alt=\"\" width=\"130\" height=\"130\" class=\"alignleft border\" />" +
            "</em>" +
            odd.oddsName +
            "</h3 > " +
            "<p>(1)" +
            odd.homeTeamName +
            ": " +
            odd.oddValues.homeOddValue +
            "</p>" +
            "<p>(X) Draw : " +
            odd.oddValues.drawOddValue +
            "</p>" +
            "<p>(2)" +
            odd.awayTeamName +
            ": " +
            odd.oddValues.awayOddValue +
            "</p>";

        ulElement.appendChild(li);
    });
});

connection.start().catch(function (error) {
    return console.error(error.toString());
});