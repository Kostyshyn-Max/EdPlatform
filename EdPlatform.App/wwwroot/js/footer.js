function isInViewport(element) {
    const bounding = element.getBoundingClientRect();
    if (bounding.top >= 0 && bounding.left >= 0 &&
        bounding.right <= (window.innerWidth || document.documentElement.clientWidth)
        && bounding.bottom <= (window.innerHeight || document.documentElement.clientHeight))
        return true;
    else 
        return false;
}

document.addEventListener('DOMContentLoaded', function () {
    let footer = document.querySelector('footer');

    console.log(isInViewport(footer));

    console.log(footer.getBoundingClientRect());

    if (isInViewport(footer))
        footer.classList.add("pinned");
});