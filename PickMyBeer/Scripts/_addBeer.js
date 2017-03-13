$().ready(function () {

    $("#getBeer").click(function () {
        var url = 'http://api.brewerydb.com/v2/beers';
        var key = 'd066193b962205c7958a875ed3aeba74';
        var queryString = $("#beer").val()
        var searchUrl = url + '?' + 'name=*' + queryString + '*&key=' + key + '&withBreweries=Y';
        var data = {
            "withBreweries": "Y",
            "withIngredients" : "Y"
        }
        $.get(searchUrl, data, function (resp) {
            console.log(resp)
            showBeers(resp.data)
        });
    });

    var showBeers = function (beers) {
        var ctrl = $("#beers")
        ctrl.empty();
        for (idx in beers) {
            var title = beers[idx].name;
            var desc = beers[idx].description;
            //trl.append("<td>" + title + "</td><td>" + desc + "</td>")
            var style = beers[idx].style.name;
            var brewery = beers[idx].breweries[0].name
            var beerId = beers[idx].id
            ctrl.append("<tr><td>" + title + "</td><td>" + desc + "</td><td>" + style + "</td><td>" + brewery + "</td><td><button id=beerId class='chooseBeer' >Choose</button></td></tr>");

            $("&chooseBeer").click(function () {
                var beer = 
                $.ajax({
                    data: MyPerson,
                    url: $(this).attr('href'),
                    type: 'POST',
                    dataType: 'json' /* this really is optional */,
                    success: function (response) {
                        return true;
                    },
                    error: function ( error ) {
                        return false;
                    }
                });
        }
    };

    
});