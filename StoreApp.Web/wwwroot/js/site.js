// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).on("click", ".searchResults", function () {
    var name = $(this).data("name");
    $("#search-text").val(name);
    $("#search-panel").hide();
    window.location.href = "/Home/Index?q=" + name;
});

$(document).on("click", "#search-button", function () {
    var name = $("#search-text").val();

    if (name.length >= 2) {
        window.location.href = "/Home/Index?q=" + name;
    }

    $("#search-panel").hide();
});

$(document).on("focus", "#search-text", function () {
    $("#search-box").css('border', '2px solid #FF6000');
    $("#search-button").css('background-color', '#FF6000');
    $("#search-panel").show();

    if ($(this).val().length >= 2) {
        ChangeSearchDatas($(this).val());
    }
});

$(document).on("blur", "#search-text", function () {
    $("#search-box").css('border', '2px solid #919191');
    $("#search-button").css('background-color', '#919191');

    setTimeout(
        function () {
            $("#search-panel").hide();
        }, 100
    );
});

function ChangeSearchDatas(searchText) {
    $("#search-panel").text("");
    $("#search-empty").hide();

    $.ajax({
        type: 'GET',
        url: "http://localhost:5292/api/StoreApp/GetSubCategoriesBySearchText/" + searchText,
        dataType: 'Json',
        data: {
            SearchText: searchText
        },
        success: function (datas) {
            if (datas.length > 0) {
                for (let i = 0; i < datas.length; i++) {
                    $("#search-panel").append(`
                        <p class="searchResults" style="cursor:pointer;" data-name="${datas[i].name}">${datas[i].name}</p>
                    `);
                }
            }
        }
    });

    $.ajax({
        type: 'GET',
        url: "http://localhost:5292/api/StoreApp/GetProductsBySearchText/" + searchText,
        dataType: 'Json',
        data: {
            SearchText: searchText
        },
        success: function (datas) {
            if (datas.length > 0) {
                for (let i = 0; i < datas.length; i++) {
                    $("#search-panel").append(`
                        <p class="searchResults" style="cursor:pointer;" data-name="${datas[i].name}">${datas[i].name}</p>
                    `);
                }
            }
        }
    });

    $.ajax({
        type: 'GET',
        url: "http://localhost:5292/api/StoreApp/GetBrandsBySearchText/" + searchText,
        dataType: 'Json',
        data: {
            SearchText: searchText
        },
        success: function (datas) {
            if (datas.length > 0) {
                for (let i = 0; i < datas.length; i++) {
                    $("#search-panel").append(`
                        <p class="searchResults" style="cursor:pointer;" data-name="${datas[i].name}">${datas[i].name}</p>
                    `);
                }
            }
        }
    });
}
