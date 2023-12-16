function getHeading() {
    var menus;
        const postContent = document.getElementById('postcontent');
        if (postContent) {
            const headings = postContent.querySelectorAll('h1, h2, h3, h4, h5, h6');
            headings.forEach((heading, index) => {
                const tagName = heading.tagName.toLowerCase();
                let id = "heading" + index;
                //heading.innerHTML = tagName.charAt(1) + " " + heading.innerHTML;
                switch (tagName) {
                    case "h1":
                        heading.classList.add("text-3xl")
                        break;
                    case "h2":
                        heading.classList.add("text-xl")
                        break;
                    case "h3":
                        heading.classList.add("text-lg")
                        break;
                    default:
                        heading.classList.add("text-base")
                }
                heading.setAttribute("id", id)
                menus.push({
                    id: id,
                    value: heading.textContent
                });
            });
        } else {
            console.log("Can't read heading");
        }
    return menus;
}