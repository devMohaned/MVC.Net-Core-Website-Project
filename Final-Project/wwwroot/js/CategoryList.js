let getAllBrands = document.getElementById("getAllBrands");
let inputGroupSelect01 = document.getElementById("inputGroupSelect01");

getAllBrands.onclick = function () {
    if (getAllBrands.checked ==true) {
        inputGroupSelect01.setAttribute("disabled", "disabled");
    } else {
        inputGroupSelect01.removeAttribute("disabled");

    }
    console.log(getAllBrands.checked);
}