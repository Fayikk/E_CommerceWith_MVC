﻿document.addEventListener("DOMContentLoaded", function () {
    const products = [];
    fetch(`https://localhost:7159/ProductClient/GetAllMyFavourite`, { method: "GET" })
        .then(response => response.json())
        .then(data => {
            for (let i = 0; i < data.length; i++) {
                products.push(data[i]);
            }
            console.log(products.length);
            var favoriteButtons = document.querySelectorAll(".btn[data-favorite]");
            favoriteButtons.forEach(button => {
                const productId = button.getAttribute("data-productId");
                const isFavorite = button.getAttribute("data-favorite");
                var icon = button.querySelector("i");
                for (let i = 0; i < products.length; i++) {
                    if (products[i].productId == productId) {
                        console.log(data[i]);
                        icon.classList.remove("fa-regular");
                        icon.classList.add("fa-solid");
                        button.setAttribute("data-favorite", "true");

                    }

                }
            })
        })




})



document.addEventListener("DOMContentLoaded", function () {
    const heartButtons = document.querySelectorAll(".btn[data-favorite]");

    heartButtons.forEach(button => {
        button.addEventListener('click', function () {
            const productId = this.getAttribute("data-productId");
            const IsFavourite = this.getAttribute("data-favorite");
            if (IsFavourite == "false") {
                this.setAttribute("data-favorite", "true");
                fetch(`https://localhost:7159/AddMyFavourites/${productId}?`, { method: "POST" })
                    .then(response => response.ok)
                    .then(data => {
                        this.setAttribute("data-favorite", "true");
                        toastr.success("Added Your Favourite This Product");
                    });
            }
            else {
                this.setAttribute("data-favorite", "false");
                fetch(`https://localhost:7159/DeleteMyFavourites/${productId}?`, { method: "DELETE" })
                    .then(response => response.ok)
                    .then(data => {
                        this.setAttribute("data-favorite", "false");
                        toastr.warning("Remove Your Favourite This Product");
                    });
            }
        });
    });
});



const outlineHearts = document.querySelectorAll('.fa-regular')
const solidHearts = document.querySelectorAll('.fa-solid')

outlineHearts.forEach(heart => {
    heart.addEventListener('click', () => {
        heart.classList.toggle('fa-solid')
    });
});

outlineHearts.forEach(heart => {
    heart.addEventListener('click', () => {
        heart.classList.toggle('fa-regular')
    });
});
