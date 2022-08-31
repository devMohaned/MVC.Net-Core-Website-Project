

function AddToFavourit(event,id) {
    let FavRequest = new XMLHttpRequest();
    FavRequest.onreadystatechange =  ()=> {
        if (FavRequest.status = 200 && FavRequest.readyState == 4) {
            if (FavRequest.responseText=="Done") {
                event.style.color = "red";
            } else if (FavRequest.responseText =="Removed") {
                event.style.color = "black";
            }
            if (FavRequest.responseText=="") {
                window.location = "/Identity/Account/Login?ReturnUrl=%2Fads%2Findex?adid=" + id;
            } 
        }
    }


    FavRequest.open("GET", "/ads/AddToFavourit/" + id)
    FavRequest.send();
}