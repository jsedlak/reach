document.addEventListener('mousedown', function (event) {
    console.log('click detected');

    if (!event.target || !event.target.matches('.resizer')) {
        return;
    }

    event.preventDefault();

    console.log('resizer click detected');

    const resizer = event.target;
    const resizable = event.target.previousElementSibling;

    const startX = event.clientX;
    const startWidth = resizable.offsetWidth;

    function onMouseMove(e) {
        const newWidth = startWidth + (e.clientX - startX);
        resizable.style.width = newWidth + 'px';
    }

    function onMouseUp() {
        console.log('onMouseUp');
        document.removeEventListener('mousemove', onMouseMove);
        document.removeEventListener('mouseup', onMouseUp);
        document.removeEventListener('dragend', onMouseUp);
    }

    document.addEventListener('mousemove', onMouseMove);
    document.addEventListener('mouseup', onMouseUp);
    document.addEventListener('dragend', onMouseUp);
});