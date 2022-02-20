// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document)
    .ready(function () {
        let connection = new signalR.HubConnectionBuilder()
            .withUrl("notificationshub")
            .build();

        connection.on('Send', (type, notification) => {
            var notificationElement = $("<div>")
                .addClass("alert alert-dismissible fade show alert-" + type)
                .html("<span>" + notification + "</span><button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button>");
            $("#notifications").append(notificationElement);

            var notesCount = parseInt($(".circle-cell").html());
            $(".circle-cell").html(notesCount + 1);

            $(".btn-close")
                .click(function () {
                    var count = $("#notifications div").length;
                    $(".circle-cell").html(count - 1);
                });

            $("#notes")
                .click(function () {
                    if ($('#notifications').css('display') == 'none') {
                        $('#notifications')
                            .css({
                                display: 'block'
                            });
                    } else {
                        $('#notifications')
                            .css({
                                display: 'none'
                            });
                    }

                });

        });

        connection.start()
            .catch(err => console.log(err.toString()))

        setTimeout(() => {
            $(".alert").alert('close');
        }, 5000);
    });