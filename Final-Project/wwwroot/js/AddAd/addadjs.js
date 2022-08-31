let CategoryId = document.getElementById("CategoryId");
let BrandId = document.getElementById("BrandId"); 
let spinner = document.getElementById("spinner");
let Brands;
let req = new XMLHttpRequest();
let Response = "";
CategoryId.onchange = 
    function GetRequest() {
        req.onreadystatechange = function () {
            BrandId.innerText = "";
            document.getElementById("spinner").style.cssText = "display:block;";
        if (req.readyState == 4 && req.status == 200 && req.responseText != "[]") {
            Response = JSON.parse(req.responseText);
            for (var i = 0; i < Response.length; i++) {
                let txtnode = document.createTextNode(Response[i].BrandName);
                let opt = document.createElement("option");
                opt.value = Response[i].BrandId;
                opt.appendChild(txtnode);
                BrandId.appendChild(opt);
            }
            spinner.style.display = "none";

            }
    }
        req.open("Get", "/ads/GetBrandforCategory/" + CategoryId.value);
    req.send();
}

