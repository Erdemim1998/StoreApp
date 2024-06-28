// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).on("click", ".searchResults", function () {
    var name = $(this).data("name");
    var lang = $(this).data("lang");
    $("#search-text").val(name);
    $("#search-panel").hide();
    window.location.href = "/Home/Index?q=" + name + "&lang=" + lang;
});

$(document).on("click", "#search-button", function () {
    let searchParams = new URLSearchParams(window.location.search);
    let lang = searchParams.get('lang');
    var name = $("#search-text").val();

    if (name.length >= 2) {
        window.location.href = "/Home/Index?q=" + name + "&lang=" + lang;
    }

    $("#search-panel").hide();
});

$(document).on("focus", "#search-text", function () {
    let searchParams = new URLSearchParams(window.location.search);
    let lang = searchParams.get('lang');
    $("#search-box").css('border', '2px solid #FF6000');
    $("#search-button").css('background-color', '#FF6000');
    $("#search-panel").show();

    if ($(this).val().length >= 2) {
        ChangeSearchDatas($(this).val(), lang);
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

function ChangeSearchDatas(searchText, lang) {
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
                        <p class="searchResults" style="cursor:pointer;" data-name="${datas[i].name}" data-lang="${lang}">${datas[i].name}</p>
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
                        <p class="searchResults" style="cursor:pointer;" data-name="${datas[i].name}" data-lang="${lang}">${datas[i].name}</p>
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
                        <p class="searchResults" style="cursor:pointer;" data-name="${datas[i].name}" data-lang="${lang}">${datas[i].name}</p>
                    `);
                }
            }
        }
    });
}

function langSelectorClick() {
    if ($(".lang-menu").is(":visible")) {
        $(".lang-menu").hide();
    }

    else {
        $(".lang-menu").show();
    }
}

function langSelectorBlur() {
    setTimeout(
        function () {
            $(".lang-menu").hide();
        }, 100
    );
}

$(document).on("click", ".lang-menu > .dropdown-item", function () {
    let lang = $(this).data("lang");
    var url = new URLSearchParams(window.location.search);
    let urlParam = url.get("url");
    let qParam = url.get("q");
    let minPriceParam = url.get("minPrice");
    let maxPriceParam = url.get("maxPrice");
    let PageIndexParam = url.get("PageIndex");
    let brandsParam = url.get("brands");
    let productId = url.get("productId");
    let categoryId = url.get("categoryId");

    if (urlParam == null && qParam == null && minPriceParam == null && maxPriceParam == null && PageIndexParam == null && brandsParam == null && productId == null && categoryId == null) {
        window.location.href = "?lang=" + lang;
    }

    else {
        if (!window.location.href.includes("&lang=")) {
            window.location.href += "&lang=" + lang;
        }

        else {
            if (lang == "en") {
                window.location.href = window.location.search.replace("&lang=tr", "&lang=" + lang);
            }

            else {
                window.location.href = window.location.search.replace("&lang=en", "&lang=" + lang);
            }
        }
    }
});