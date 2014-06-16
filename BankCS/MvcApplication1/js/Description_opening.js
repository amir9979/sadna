window.onload = function () {
          var rotator = document.getElementById("rotator");
          var images = rotator.getElementsByTagName("img");
          for (var i = 1; i < images.length; i++) {
              images[i].style.display = "none";
          }
          var counter = 1;
          setInterval(function () {
              for (var i = 0; i < images.length; i++) {
                  images[i].style.display = "none";
              }
              images[counter].style.display = "block";
              counter++;
              if (counter == images.length) {
                  counter = 0;
              }
          }, 4000);
	
		mySound=document.getElementById("sound");
		mySound.volume=0.2;
};