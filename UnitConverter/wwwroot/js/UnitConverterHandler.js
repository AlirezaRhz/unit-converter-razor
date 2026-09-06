const tabButtons = document.querySelectorAll('.unit-btn')
const CategoryInput = document.getElementById("CategoryInput")

tabButtons.forEach(button => {
    if (button.dataset.category == CategoryInput.value) {
        button.classList.add("selected-unit-of-measurement")
    }
    else {
        button.classList.remove("selected-unit-of-measurement")
    }
})

tabButtons.forEach(button => {
    button.addEventListener("click", () => {
        
    const category = button.dataset.category
    CategoryInput.value = category
    window.location.href = `${window.location.pathname}?Category=${category}`
    })
});

