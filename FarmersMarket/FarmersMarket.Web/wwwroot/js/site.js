// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document)
    .ready(function () {
        //$.ajax({
        //    url: '@Url.Action("GetCategories", "Categories", new { Area = "Admin" })',
        //    type: "GET",
        //    success: function (result) {

        //        $.each(result, function (key, value) {
        //            var category = value.Name;
        //            var li = $('<li role="presentation">');
        //            var link = $('<a href="/products/' + category.toLowerCase() + '">' + category + '</div>');
        //            li.append(link);
        //            $(".dropdown ul").append(li);
        //        });
        //    }
        //});

        //var notificationsHub = $.connection.notifications;
        //notificationsHub.client.receiveNotification = function (type, notification) {
        //    var notificationElement = $("<div>")
        //        .addClass("alert alert-dismissible alert-" + type)
        //        .html("<button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>" +
        //            notification);
        //    $("#notifications").append(notificationElement);

        //    var notesCount = parseInt($(".circle-cell").html());
        //    $(".circle-cell").html(notesCount + 1);

        //    $(".close")
        //        .click(function () {
        //            var count = $("#notifications div").length;
        //            $(".circle-cell").html(count - 1);
        //        });

        //    $("#notes")
        //        .click(function () {
        //            if ($('#notifications').css('display') == 'none') {
        //                $('#notifications')
        //                    .css({
        //                        display: 'block'
        //                    });
        //            } else {
        //                $('#notifications')
        //                    .css({
        //                        display: 'none'
        //                    });
        //            }

        //        });
        //};

        //$.connection.hub.start();

        setTimeout(() => {
            $(".alert").alert('close');
        }, 5000);
    });
