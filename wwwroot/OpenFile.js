function fileOpenAs() {
    var msg = "exit";

    if ('launchQueue' in window) {
        console.debug('File handling API is supported!');
        launchQueue.setConsumer((launchParams) => {
            // Nothing to do when the queue is empty.
            if (!launchParams.files.length) {
                msg = "empty";
                return msg;
            }
            // Handle the first file only.
            fileContentAs(launchParams.files[0]);
        });
        return msg;
    } else {
        console.debug('File handling API is not supported!');
        return "not supported";
    }
    async function fileContentAs(fileHandle) {

        msg = "waiting";
        const blob = await fileHandle.getFile();
        blob.handle = fileHandle;
        const content = await blob.text();

        DotNet.invokeMethodAsync('FMV_Standard', 'FileContent', content, fileHandle.name);
    }
}