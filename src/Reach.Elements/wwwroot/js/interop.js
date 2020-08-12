// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

window.outsideClickHandler = {
    addEvent: function (elementId, dotnetHelper) {
        window.addEventListener("click", (e) => {
            if (!document.getElementById(elementId).contains(e.target)) {
                console && console.log("clicked outside");
                dotnetHelper.invokeMethodAsync("InvokeClickOutside");
            }
        });
    }
};