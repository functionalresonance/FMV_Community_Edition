function FileSaveAs(filename, fileContent) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;charset=utf-8," + encodeURIComponent(fileContent)
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}