var theParent = document.querySelector("#images");
theParent.addEventListener("click", Zoom, false);

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// Get the modal
var modal = document.getElementById('myModal');

var captionText = document.getElementById("caption");

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
};

function Zoom(e) {

    if (e.target !== e.currentTarget) {
        if (e.target === span) {
            modal.style.display = "none";
            e.stopPropagation();
            return;
        }

        // alert(e.target.tagName);
        if (e.target.tagName === 'IMG') {
            var clickedItem = e.target.id;
            // alert("Hello " + clickedItem);

            // Get the image and insert it inside the modal - use its "alt" text as a caption
            var img = document.getElementById(clickedItem);
            var modalImg = document.getElementById("img01");

            modal.style.display = "block";
            modalImg.src = img.src;
            captionText.innerHTML = img.title;

        }
    }

    e.stopPropagation();
}


