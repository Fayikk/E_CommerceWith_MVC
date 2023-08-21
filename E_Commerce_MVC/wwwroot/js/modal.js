
    var openModalBtns = document.querySelectorAll(".openModalBtn");
    var modals = document.querySelectorAll(".modal");
    var closeButtons = document.querySelectorAll(".close-button");
    console.log("deneme");


    for(let i = 0; i < openModalBtns.length;i++){
        openModalBtns[i].addEventListener("click", function () {
            console.log("deneme" + $`{i}`);
            modals[i].style.display = "block";
        })
    }



    closeButtons.forEach(function (closeBtn) {
        closeBtn.addEventListener("click", function () {
            modals.forEach(function (modal) {
                modal.style.display = "none";
            });
        });
    });

    window.addEventListener("click", function (event) {
        modals.forEach(function (modal) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        });
    });
