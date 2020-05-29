function sumar() {
    var monto = document.getElementById('monto').value;
    var total = 0;
    total = parseInt(monto); //obtengo la cantidad a apostar

    var eleccion = document.getElementById('eleccion');
    var item = eleccion.options[eleccion.selectedIndex].value;
    alert(monto);
    if (item == 'Local')
        total = (total * parseFloat(@TempData["Local"].ToString())) - parseInt(monto);
            else if (item == 'Empate')
        total = (total * parseFloat(@TempData["Empate"].ToString())) - parseInt(monto);
            else
    total = (total * parseFloat(@TempData["Visita"].ToString())) - parseInt(monto);

    // Aquí valido si hay un valor previo, si no hay datos, le pongo un cero "0".
    //total = (total == null || total == undefined || total == "") ? 0 : total;

    /* Esta es la suma. */
    //total = (parseInt(total) + parseInt(valor));

    // Colocar el resultado de la suma en el control "span".
    document.getElementById('premio').innerHTML = total;
}