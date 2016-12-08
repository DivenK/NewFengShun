$(function ()
{
    var $modal = $('#doc-modal-1');

    $('#commerceAdd').on('click', function (e) {

        SetNewModel(0, "", "");
        //使用详情看 http://amazeui.org/javascript/modal

        $modal.modal({
            width: 800,
            height: 650
        });

    });
    $('#Cancel').on('click', function () {
        $modal.modal('close');
    });
    $('.editCommerce').on('click', function ()
    {
        var id = $(this).attr('data-id');
        $.ajax({
            url: "../Commerce/GetModel",
            data: { id: id },
            type: "post",
            dataType: 'json',
            success: function (result) {
                SetNewModel(result.id, result.Name, result.Contents)
                $modal.modal({
                    width: 800,
                    height: 650
                });
            },
            error: function (er) {
                BackErr(er);
            }
        });
    });

    //修改和新增的保存
    $('#formSubmit').click(function () {
        var id = 0;
        id = $(this).attr('date-id');
        var newModel = {
            id: 0,
            Name: $('#doc-ipt-text-1').val(),
            Conent: editor.getContent(),
        };
        $.post("../Commerce/UpdateCommerce",
            { "Conent": newModel.Conent, "Name": newModel.Name,  "id": id },
            function (result) {
                $modal.modal('close');
                if (result.status == 0) {
                    var message = '添加成功';
                    if (id > 0) {
                        message = '编辑成功';
                    }
                    bfeMsgBox.success("", message);
                }
                else {
                    bfeMsgBox.error("", result.msg);
                }
                AjaxGetList(1, 0);
            });
    });

    //删除
    $('.delCommerce').on('click',(function () {
        var id = $(this).attr('data-id');
        $('#my-confirm-Del').modal({
            relatedTarget: this,
            onConfirm: function (options) {
                $.ajax({
                    url: "../Commerce/Delete",
                    data: { id: id },
                    type: "get",
                    dataType: 'json',
                    success: function (result) {
                        bfeMsgBox.success("", result.msg);
                        AjaxGetList(1, 0);
                    },
                    error: function (er) {
                        bfeMsgBox.error("", result.msg);
                    }
                });
            },
            // closeOnConfirm: false,
            onCancel: function () {

            }
        });
    }));


});

//控制网站是否显示商会章程
///开关监控
$(document).on('click', '.am-switch', function () {
    var $checkbox = $(this).find("input[type='checkbox']");
    var state = $checkbox.is(':checked');
    $(this).css({
        'transition-duration': '0.2s'
    });
    $checkbox.prop("checked", !state);
    if (state) {
        $(this).removeClass('am-active');
        UpdateLook(this, 1);
    } else {
        $(this).addClass('am-active');
        UpdateLook(this, 0);
    }
});

function UpdateLook(e, val)
{
    id = $(e).parent('td').attr('data-id')
    $.ajax({
        url: "../Commerce/UpdateLook",
        data: { id: id, Look: val },
        type: "post",
        dataType: 'json',
        success: function (result) {
            bfeMsgBox.success("", result.msg);
            AjaxGetList(1, 0);
        },
        error: function (er) {
            bfeMsgBox.error("", result.msg);
            AjaxGetList(1, 0);
        }
    });
}
function SetNewModel(id,title,content)
{
    $('#newTitle').text("编辑");
    $('#formSubmit').attr('date-id', id);
  
    $('#doc-ipt-text-1').val(title);
    editor.setContent(content);
}

