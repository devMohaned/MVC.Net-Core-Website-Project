let request = new XMLHttpRequest();

function PhoneRequestd(id) {


    request.open("GET", "/ads/PhoneRequestedApi/" + id);
    request.send();
}