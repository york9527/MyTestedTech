$(function () {
    $('#btUploadFile').on('click', function () {
        $.ajaxFileUpload(
            {
                url: 'Upload',
                secureuri: false,
                fileElementId: 'file2',
                dataType: 'json',
                success: function (data, status) {
                    if (typeof(data.msg) != 'undefined') {
                        $('<span>' + data.msg + '</span>').insertAfter('#file2');
                    }
                },
                error: function(data, status, e) {
                    alert(e);
                }
            }
        );
        return false;
    });
});