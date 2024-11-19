$(function() {
    var page = $("#page").val();
    $.ajax({
        dataType: "json",
        url: "/AdoptHeart/GetInitialFilterValues",
        success: function (data) {
            $("#gender").val(data.gender);
            $("#age").val(data.ageGroup);
            $("#giftType").val(data.giftType);
        },
        error: function (jqXHR, exception) {
            window.location.href = "/Error";
        }

    });

    $("#gender").change(function () {
        page = 1;
        $.ajax({
            dataType: "json",
            url: "/AdoptHeart/FilterOnGender",
            data: { "gender": $(this).val() },
            type: 'POST',
            success: function(data) {
                writeHearts(data.results);
            },
            error: function(jqXHR, exception) {
                window.location.href = "/Error";
            }

        });
    });
    $("#age").change(function () {
        page = 1;
        $.ajax({
            dataType: "json",
            url: "/AdoptHeart/FilterOnAge",
            data: { "ageGroup": $(this).val() },
            type: 'POST',
            success: function(data) {
                writeHearts(data.results);
            },
            error: function(jqXHR, exception) {
                window.location.href = "/Error";
            }

        });
    });
    $("#giftType").change(function () {
        page = 1;
        $.ajax({
            dataType: "json",
            url: "/AdoptHeart/FilterOnGiftType",
            data: { "giftType": $(this).val() },
            type: 'POST',
            success: function(data) {
                writeHearts(data.results);
            },
            error: function(jqXHR, exception) {
                window.location.href = "/Error";
            }

        });
    });

    $("#clear").click(function () {
        page = 1;
        $.ajax({
            url: "/AdoptHeart/Clear",
            success: function(data) {
                $("#gender").val("");
                $("#age").val("");
                $("#giftType").val("");
                writeHearts(data.results);
            },
            error: function(jqXHR, exception) {
                window.location.href = "/Error";
            }
        });
    });

    $(document.body).on("click",
        ".pageBack",
        function () {
            page = parseInt(page) - 1;
            $.ajax({
                url: "/AdoptHeart/Page/" + page,
                success: function (data) {
                    writeHearts(data.results);
                },
                error: function (jqXHR, exception) {
                    window.location.href = "/Error";
                }
            });
        });


    $(document.body).on("click",
        ".pageForward",
        function () {
            page = parseInt(page) + 1;
            $.ajax({
                url: "/AdoptHeart/Page/" + page,
                success: function (data) {
                    writeHearts(data.results);
                },
                error: function (jqXHR, exception) {
                    window.location.href = "/Error";
                }
            });
        });

    $(document.body).on("click",
        ".page",
        function () {
            var id = $(this).attr("id");
            page = parseInt(id);
            $.ajax({
                url: "/AdoptHeart/Page/" + page,
                success: function (data) {
                    writeHearts(data.results);
                },
                error: function (jqXHR, exception) {
                    window.location.href = "/Error";
                }
            });

        });

    function writePager() {
        $("#pagination").html("");
        var count = parseInt($("#count").val());
        var strHtml = "";
        var t = count / parseFloat($("#pageSize").val());
        var c = Math.ceil(t);
        var t2;
        if (c < 6) {
            t2 = 1;
        } else {
            t2 = c - 4;
        }

        var startingPoint = Math.min(page, t2);
        var endingPoint = Math.min(5 + page, c + 1);
        if (page > 1) {
            strHtml += "<li class='list-group-item'><a class='pageBack' ref='#'><</a></li>";
        }
        for (var i = startingPoint; i < endingPoint; i++) {

            if (i === page) {
                strHtml += "<li class='active list-group-item'><a ref='#'>" + i + "</a></li>";
            } else {
                strHtml += "<li class='list-group-item'><a class='page' ref='#' id='" + i + "'>" + i + "</a></li>";
            }

        }
        if (page < c) {
            strHtml += "<li class='list-group-item'><a class='pageForward' ref='#'>></a></li>";
        }

        $("#pagination").html(strHtml);
    }


    function writeHearts(results) {
        $("#hearts").empty();
        var strHtml = "";
        for (var i = 0; i < results.length; i += 3) {
            strHtml += "<div class='row'>";
            for (var j = i; j <= Math.min(2 + i, results.length - 1); j++) {
                strHtml += "<div class='col-md-4'>";
                strHtml += "<a href='/AdoptHeart/AdoptHeart/" + results[j].recipientNumber + "' style='text-decoration:none'>";
                strHtml += "<div class='heart' style='height: 300px'>";
                strHtml += "<div class='intro-message'>";
                strHtml += "<div class='italicize big-heart-text'>Secret Santa " + results[j].yearString + "</div>";
                strHtml += "<div class='md-heart-text'>" + results[j].nameAgeGenderString + "</div>";
                strHtml += "<div class='md-heart-text'>" + results[j].recipientInfo + "</div>";
                strHtml += "<div class='sm-heart-text'>First Wish: " + results[j].firstWishString + "</div>";
                strHtml += "<div class='sm-heart-text'>Second Wish: " + results[j].secondWishString + "</div>";
                strHtml += "<div class='big-text bold'>R# " + results[j].recipientNumber + "</div>";
                strHtml += " </div>";
                strHtml += " </div>";
                strHtml += "</a>";
                strHtml += " </div>";
            }
            strHtml += " </div>";
        }
        $("#hearts").append(strHtml);
        writePager();
        getFilterValues();
    }

    function getFilterValues() {
        $.ajax({
            dataType: "json",
            url: "/AdoptHeart/GetInitialFilterValues",
            success: function (data) {
                $("#gender").val(data.gender);
                $("#age").val(data.ageGroup);
                $("#giftType").val(data.giftType);
            },
            error: function (jqXHR, exception) {
                window.location.href = "/Error";
            }

        });
    }

});