$(document).ready(function () {

	var host = "https://" + window.location.host;
	
	var headers = {};
	var editId;
	var moviesEndpoint = "/api/movies";
	var directorsEndpoint = "/api/directors";


	// pripremanje dogadjaja za brisanje
	$("body").on("click", "#btnDelete", deleteMovie);

	// priprema dogadjaja za izmenu
	$("body").on("click", "#btnEdit", editMovie);


	
	loadMovies();
	
	
	//funckija ucitavanja filmova
	function loadMovies() {
		var requestUrl = host + moviesEndpoint;
		$.getJSON(requestUrl, setMovies);
	}
	

	// metoda za postavljanje filmova u tabelu
	function setMovies(data, status) {

		var $container = $("#data");
		$container.empty();

		if (status === "success") {
			// ispis naslova
			var div = $("<div></div>");
			var h1 = $("<h1 style='text-align:center'>Movies</h1>");
			div.append(h1);
			// ispis tabele
			var table = $("<table class='table table-bordered' ></table>");
			
			
			var	header = $("<thead><tr style='background-color:silver'><td>Id</td><td>Name</td><td>Genre</td><td>Year</td><td>Director</td><td>Delete</td><td>Edit</td></tr></thead>");
			

			table.append(header);
			tbody = $("<tbody></tbody>");
			for (i = 0; i < data.length; i++) {
				// prikazujemo novi red u tabeli
				var row = "<tr>";
				// prikaz podataka
				var displayData = "<td>" + data[i].Id + "</td><td>" + data[i].Name + "</td><td>" + data[i].Genre + "</td><td>" + data[i].Year + "</td><td>" + data[i].Director.Name + " "+data[i].Director.Surname + "</td>";
				// prikaz dugmadi za izmenu i brisanje
				var stringId = data[i].Id.toString();
				var displayDelete = "<td><button class='btn btn-danger' id=btnDelete name=" + stringId + ">Delete</button></td>";
				var displayEdit = "<td><button class='btn btn-warning' id=btnEdit name=" + stringId + ">Edit</button></td>";
				// prikaz samo ako je korisnik prijavljen
				
					row += displayData + displayDelete + displayEdit + "</tr>";
				
				// dodati red
				tbody.append(row);
			}
			table.append(tbody);

			div.append(table);

			$container.append(div);
		}
	}



	//popunjavanje selekta za dodavanje
	$.ajax({
		"type": "GET",
		"url": host + directorsEndpoint
	}).done(function (data, status) {
		var select = $("#director");
		for (var i = 0; i < data.length; i++) {
			var option = "<option value='" + data[i].Id + "'>" + data[i].Name + " " + data[i].Surname + "</option>";
			select.append(option);
		}
	});

	//popunjavanje selekta za edit
	$.ajax({
		"type": "GET",
		"url": host + directorsEndpoint
	}).done(function (data, status) {
		var select = $("#editDirector");
		for (var i = 0; i < data.length; i++) {
			var option = "<option value='" + data[i].Id + "'>" + data[i].Name + " " + data[i].Surname + "</option>";
			select.append(option);
		}
	});

	

	

	// dodavanje novog filma
	$("#addMovieForm").submit(function (e) {
		// sprecavanje default akcije forme
		e.preventDefault();
		

		var name = $("#nameMovie").val();
		var genre = $("#genre").val();
		var year = $("#year").val();
		var director = $("#director").val();

		var sendData = {
			"Name": name,
			"Genre": genre,
			"Year": year,
			"DirectorId": director
		};

		$.ajax({
			"type": "POST",
			"url": host + moviesEndpoint,
			"headers": headers,
			"data": sendData
		})
			.done(function (data, status) {
				$("#nameMovie").val("");
				$("#genre").val("");
				$("#year").val("");
				$("#director").val("1");

				loadMovies();
			}).fail(function (data, status) {
				alert("Error!");
			});
	});
	


	// dodavanje novog rezisera
	$("#addDirectorform").submit(function (e) {
		// sprecavanje default akcije forme
		e.preventDefault();


		var name = $("#nameDirector").val();
		var surname = $("#surname").val();
		var birth = $("#birth").val();
		

		var sendObject = {
			"Name": name,
			"Surname": surname,
			"Birth": birth
			
		};

		$.ajax({
			type: "POST",
			url: host + directorsEndpoint,
			headers: headers,
			data: sendObject
		})
			.done(function (data, status) {
				$("#nameDirector").val("");
				$("#surname").val("");
				$("#birth").val("");
				

				loadDirectors();
			}).fail(function (data, status) {
				alert("Error!");
			});
	});
	
	

	

	

	//poziv edit funkcije
	function editMovie() {
		$("#editMovieForm").css("display", "block");
		editId = this.name;

		

		$.ajax({
			"type": "GET",
			"url": host + moviesEndpoint + "/" + editId,
			"headers": headers
		}).done(function (data, status) {

			console.log(data);

			$("#editMovieName").val(data.Name);
			$("#editGenre").val(data.Genre);
			$("#editYear").val(data.Year);
			$("#editDirector").val(data.DirectorId);
			

		});
	}

	//edit filma
	$("#editMovieForm").submit(function (e) {
		e.preventDefault();

		var name = $("#editMovieName").val();
		var genre = $("#editGenre").val();
		var year = $("#editYear").val();
		var director = $("#editDirector").val();

		var sendData = {
			"Id": editId,
			"Name": name,
			"Genre": genre,
			"Year": year,
			"DirectorId": director

		};

		$.ajax({
			"type": "PUT",
			"url": host + moviesEndpoint + "/" + editId,
			"data": sendData,
			"headers": headers
		}).done(function () {
			loadMovies();
			$("#editMovieForm").css("display", "none");
		}).fail(function () { alert("Error!"); });



	});

	//izlaz iz edita
	$("#back").click(function () {
		
		$("#editMovieForm").css("display", "none");

	});


	
	//brisanje filma
	function deleteMovie() {

		var deleteID = this.name;



		// saljemo zahtev 
		$.ajax({
			url: host + moviesEndpoint + "/" + deleteID.toString(),
			type: "DELETE",
			headers: headers
		})
			.done(function (data, status) {
				loadMovies();
			})
			.fail(function (data, status) {
				alert("Error!");
			});
	}


		

	

	

});
