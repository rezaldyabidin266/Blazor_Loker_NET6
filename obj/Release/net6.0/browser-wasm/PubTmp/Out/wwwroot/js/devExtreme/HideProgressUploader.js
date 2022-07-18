window.HideProgressPanel = function () {
    this.setTimeout(function () {
        var elem = this.document.querySelectorAll(".fileUploaderProfile .dxuc-file-view")[0];
        elem.classList.add("d-none");
    }, 1000);

}
window.ShowProgressPanel = function () {
    var elem = this.document.querySelectorAll(".fileUploaderProfile .dxuc-file-view")[0];
    elem.classList.remove("d-none");
}