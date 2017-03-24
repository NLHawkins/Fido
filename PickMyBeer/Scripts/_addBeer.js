$().ready(function () {


    var beers = null;
    var page = 0;
    var maxPage = 0;
    var url = 'http://api.brewerydb.com/v2/search';
    var key = 'd066193b962205c7958a875ed3aeba74';

    //***Get Beers Initial Search

    $("#searchBeersByName").click(function () {
        page = 1;
        var queryString = $("#beerN").val()
        var url = 'http://api.brewerydb.com/v2/search';
        var searchUrl = url + '?' + 'q=' + queryString + '&key=' + key + '&p=' + page + '&type=beer';
        console.log(searchUrl);
        var data = {
            "withBreweries": "Y",
            "withIngredients": "Y",

        }
        $.get(searchUrl, data, function (resp) {
            beers = resp.data;
            page = resp.currentPage;
            maxPage = resp.numberOfPages
            console.log(resp)
            showSBeers(resp.data)
        });

    });

    $("#nextPage").click(function () {
        if (page < maxPage) {
            page++;
            var queryString = $("#beerN").val();
            var searchUrl = url + '?' + 'q=' + queryString + '&key=' + key + '&p=' + page + '&type=beer';
            console.log(searchUrl);
            var data = {
                "withBreweries": "Y",
                "withIngredients": "Y",
            }
            $.get(searchUrl, data, function (resp) {
                beers = resp.data;
                console.log(resp)
                showSBeers(resp.data)
            });
        }
        else {
            alert("Next Page does not exsist")
        }

    });

    $("#prevPage").click(function () {
        if (page > 1) {
            page--;
            var queryString = $("#beerN").val()
            var searchUrl = url + '?' + 'q=' + queryString + '&key=' + key + '&p=' + page + '&type=beer';
            console.log(searchUrl);
            var data = {
                "withBreweries": "Y",
                "withIngredients": "Y",
            }
            $.get(searchUrl, data, function (resp) {
                beers = resp.data;
                console.log(resp)
                showSBeers(resp.data)
            });
        }
        else {
            alert("There is no Previous Page")
        }

    });

    $("#goToPage").click(function () {
        var queryString = $("#beerN").val();
        page = $("#page").val();
        var searchUrl = url + '?' + 'q=' + queryString + '&key=' + key + '&p=' + page + '&type=beer';
        console.log(searchUrl);
        var data = {
            "withBreweries": "Y",
            "withIngredients": "Y",
        }
        $.get(searchUrl, data, function (resp) {
            beers = resp.data;
            console.log(resp)
            showSBeers(resp.data)
        });
    });
    var showSBeers = function (beers) {
        var ctrl = $("#searchBeers")
        ctrl.empty();
        for (idx in beers) {
            var name = beers[idx].name;
            var desc = beers[idx].description;
            var abv = beers[idx].abv;
            var ibu = beers[idx].ibu;

            var style = "";
            if (beers[idx].style !== undefined) {
                style = beers[idx].style.name;
            } else {
                style = "n/a"
            }

            var brewery = "";
            if (beers[idx].breweries !== undefined) {
                brewery = beers[idx].breweries[0].name
            } else {
                brewery = "n/a"
            }

            var beerId = beers[idx].id
            var beer = beers[idx]
                ctrl.append("<tr><td>" + name + "</td><td>" + desc + "</td><td>" + style + "</td><td>" + brewery + "</td><td><button data-bIdx =" + idx + " class='chooseBeer'>Choose</button></td></tr>");

            
        };

        $body = $("body");
        $(document).on({
            ajaxStart: function () { $body.addClass("loading"); },
            ajaxStop: function () { $body.removeClass("loading"); }
        });

        $(".chooseBeer").click(function () {
            var idx = $(this).attr("data-bIdx");
            //var jsonData = beers[idx];
            var jsonData = JSON.stringify(beers[idx]);
            //console.log(jsonData);
            console.log("*********Added Beer*************")
            $.ajax({
                url: 'http://localhost:54414/Beer/AddBeerToFaves',
                contentType: 'application/html; charset=utf-8',
                dataType: "json",
                type: 'POST',
                data: jsonData

            }).done(function () {
                window.location.replace("http://localhost:54414/AppUser/MyFaves");
            })
        });
    };
});