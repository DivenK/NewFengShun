(function () {
    var $modal = $('#doc-modal-1');

    $('#commerceAdd').on('click', function (e) {

        SetNewModel(0, "", "");
        //使用详情看 http://amazeui.org/javascript/modal

        $modal.modal({
            width: 800,
            height: 650
        });
    });
})