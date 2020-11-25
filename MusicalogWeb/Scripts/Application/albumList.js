$(function () { //DOM Ready
});

/*
    Redirect browser to Album Edit view to create a new album.
*/
function addAlbum() {
    var url = $('input[id*=hfAlbumEditURL]').val();
    window.location = url;
}

function editAlbum(albumID) {
    var url = $('input[id*=hfAlbumEditURL]').val();
    window.location = url + "/" + albumID;
}

function deleteAlbum(albumID) {
    if (confirm("Are you sure you wish to delete album?")) {
        var urlDelete = $('input[id*=hfAlbumDeleteURL]').val();
        var success = false;

        $.ajax({
            url: urlDelete,
            type: "GET",
            async: false,
            data: { albumID: albumID },
            dataType: "json",
            beforeSend: function () {
                $('body').css('cursor', 'progress');
            },
            success: function (data, textStatus, jqXHR) {
                if (data.Success) {
                    success = true;
                    alert("Album was successfully deleted.");
                }
                else {
                    alert(data.Message);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("Could not delete album. " + errorThrown);
            },
            complete: function (jqXHR, textStatus) {
                $('body').css('cursor', 'auto');
            }
        });

        // If album was successfully deleted then refresh the AlbumList.
        if (success) {
            window.location = $('input[id*=hfAlbumListURL]').val();
        }
    }
}
