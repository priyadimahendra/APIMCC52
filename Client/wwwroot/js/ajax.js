//$.ajax({
//    url: "https://pokeapi.co/api/v2/pokemon/"
//}).done((result) => {
//    console.log(result.results);

//    //ini untuk 
//    let text = '';
//    let number = 0;
//    $.each(result.results, function(key, val) {
//        text += `<tr>
//                <td>${number + 1}</td>
//                <td>${val.name}</td>
//                <td>${val.url}</td>
//                <td>
//                    <button type="button" class="btn btn-primary" data-role="detail" data-toggle="modal" data-id="${number}" data-target="#modal">
//                         detail
//                    </button>
//                </td>
//                </tr>`;
//        number++;
//    });
//    $('#listsw').html(text);
//}).fail((error) => {
//    console.log(error);
//});


let t = $('#table').DataTable();
$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon"
}).done((result) => {
    console.log(result);
    text = "";
    no = 1;
    $.each(result.results, function (key, val) {
        //text += "<li>" + val.name + "</li>";
        t.row.add([
            no,
            val.name,
            val.url,
            `<button class="btn btn-primary modalClass"  data-id="${val.url}" data-toggle="modal" data-target="#exampleModal">
                Detail
            </button>`
        ]).draw(false)
        no++;
    })
}).fail((error) => {
    console.log(error);
});

$(document).on("click", ".modalClass", function () {
    let pokemonURL = $(this).data('id');
    $.ajax({
        url: pokemonURL
    }).done((result) => {
        $("#img").html('<img class=" w-50" src="' + result.sprites.other.dream_world.front_default + '" alt="" />'); //menampilkan foto di API
        //$("#titleModelPokemon").html("Detail " + result.name); //judul di MODAL
        $("#namePokemon").html(result.name); //menampilkan dalam bentuk html untuk nama pokemon
        $("#heightPokemom").html(result.height + ' cm' ); //menampilkan tinggi pokemon
        $("#weightPokemon").html(result.weight + ' Kg'); //menampilkan berat pokemon

        //=====================================================================================
        //untuk menampilkan ability
        abilityPokemon = "";
        $.each(result.abilities, function (key, val) {
            abilityPokemon += `<span>${val.ability.name}</span> `;
        })
        $("#abilitiesPokemon").html(abilityPokemon);

        //=====================================================================================
        //untuk menampilkan type
        typePokemon = "";
        $.each(result.types, function (key, val) {
            typePokemon += `<span>${val.type.name}</span><br> `;
        })
        $("#typePokemon").html(typePokemon);

    }).fail((error) => {
        console.log(error);
    });
});

