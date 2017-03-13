$().ready(function () {
    var beers = null;
    $("#getBeer").click(function () {
        var url = 'http://api.brewerydb.com/v2/beers';
        var key = 'd066193b962205c7958a875ed3aeba74';
        var queryString = $("#beer").val()
        var searchUrl = url + '?' + 'name=*' + queryString + '*&key=' + key + '&withBreweries=Y';
        var data = {
            "withBreweries": "Y",
            "withIngredients": "Y"
        }
        $.get(searchUrl, data, function (resp) {
            beers = resp.data;
            console.log(resp)
            showBeers(resp.data)
        });
    });

    var showBeers = function (beers) {
        var ctrl = $("#beers")
        ctrl.empty();
        for (idx in beers) {
            var name = beers[idx].name;
            var desc = beers[idx].description;
            var abv = beers[idx].abv;
            var ibu = beers[idx].ibu;
            //trl.append("<td>" + title + "</td><td>" + desc + "</td>")
            var style = beers[idx].style.name;
            var brewery = beers[idx].breweries[0].name
            var beerId = beers[idx].id
            var beer = beers[idx]
            ctrl.append("<tr><td>" + name + "</td><td>" + desc + "</td><td>" + style + "</td><td>" + brewery + "</td><td><button data-bIdx =" + idx + " class='chooseBeer'>Choose</button></td></tr>");
            addBeers();
            
        };
    };

    var addBeers = function () {
    $(".chooseBeer").click(function () {
        var idx = $(this).attr("data-bIdx");
            //var jsonData = beers[idx];
            var jsonData = JSON.stringify(beers[idx]);
            console.log(jsonData);
            $.ajax({
                url: 'http://localhost:54414/Beer/AddBeer?=' ,
                contentType: 'application/html; charset=utf-8',
                dataType: "json",
                type: 'POST',
                data: jsonData

            })
        });
    }
    
});