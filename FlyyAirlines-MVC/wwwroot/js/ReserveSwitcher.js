const reserveLayout = document.getElementById("quickReserve");

const getToggle = document.getElementById("toggleSwitcher");

getToggle.checked = false;

function toggleSwitch() {
    if (getToggle.checked == true) {
        reserveLayout.style.display = "block";
    }
    else {
        reserveLayout.style.display = "none";
    }
}

getToggle.addEventListener("click", toggleSwitch);

