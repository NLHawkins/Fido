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
            console.log(beers[0]);
        });

        var addBeer = function () {
            var jsonData = JSON.stringify(beers[0]);
            //console.log(jsonData);
            console.log("*********Added Beer*************")
            $.ajax({
                url: 'http://localhost:54414/Beer/AddBeerToArchive?=',
                contentType: 'application/html; charset=utf-8',
                dataType: "json",
                type: 'POST',
                data: jsonData
            })
        };
        
    });
});