var addFlag = 0;
var editId = 0;
var pageIndex = 1;
var pageSize = 15;


$(function () {
    //控制新增的弹层
    var $modal = $('#doc-modal-1');//弹窗div  

    $('#btnAddManage').on('click', function (e) {
        //修改新增标识
        addFlag = 0;
        $modal.modal({ closeViaDimmer: 0, width: 800, height: 800 });//弹起
    });

    //  GetContentByPage(1,15,"");

    /******************************
     1.0 修改或者新增保存点击事件
    ******************************/
    $("#formSubmit").unbind("click").bind("click", function () {
        var title = $("#doc-title").val();
        if (title == "") {
            $("#doc-title").focus();
            return;
        }
        var contentModel = {
            "flag": addFlag,
            "id": 0,
            "title": title,
            "conter": editor.getContent(),
            "author": $("#doc-author").val()
        }
        $.ajax({
            url: "/admin/manage/change",
            data: contentModel,
            type: "post",
            dataType: 'json',
            success: function (retContent) {
                if (retContent.status == 0) {
                    bfeMsgBox.success("", retContent.msg);
                    window.location.reload();
                }
                else {
                    bfeMsgBox.error("", retContent.msg);
                }
            },
            error: function (er) {
                bfeMsgBox.error("", er);
            }
        });
    })


    /****************************************
    2.0 富文本编辑器取消
    ****************************************/
    $("#Cancel").unbind("click").bind("click", function () {
        $modal.modal('close');
    })


    /****************************************
    3.0 搜索查询数据
    ****************************************/
    $("#btnSearch").unbind("click").bind("click", function () {
        var searchContent = $("#txtSearchContent").val();
        //if (searchContent == "") {
        //    bfeMsgBox.error("", "请输入查询条件");
        //    $("#txtSearchContent").focus();
        //    return;
        //}
        GetContentByPage(pageIndex, pageSize, searchContent)
    })


})
   /****************************************
   4.0 开启或者关闭评论功能
   ****************************************/
$(document).on('click', '.changelook', function () {
    var content = {
        "id": $(this).find("input[type=checkbox]").attr("data-id")
    }    
    $.ajax({
        url: "/admin/manage/ChangeLook",
        data: content,
        type: "post",
        datatype: "json",
        success: function (e) {
            if (e.status == 0) {
                bfeMsgBox.success("", "操作成功");
            }
            else {
                bfeMsgBox.error("", "操作失败");
                setTimeOut(function () { window.location.reload(); }, 1000);
            }
        },
        error: function (er) {
            bfeMsgBox.error("", er);
        }
    })
});

 /****************************************
   编辑
  ****************************************/
$(document).on("click", ".edit-model", function () {
    alert();
})

    /***********************
    获取数据
    ***********************/
    function GetContentByPage(pageIndex, pageSize, searchMsg) {
        var content = {
            "pageIndex": pageIndex,
            "PageSize": pageSize,
            "searchContent": searchMsg
        };
        $.post("/Admin/Manage/GetManageData", content, function (retContent) {
            if (retContent.status == 0) {
                var totalPage = retContent.datas.TotalPage;
                var dataContent = retContent.datas.Content;

                $("#new-pageCount").text(totalPage);
                //TODO：将totalPage赋值给分页插件

                //将数据展示到页面
                var showHtml = "";
                for (var i = 0; i < dataContent.length; i++) {
                    showHtml += '<tr>';
                    showHtml += '<td>' + (i + 1) + '</td>';
                    showHtml += '<td>' + dataContent[i].Title + '</td>';
                    showHtml += '<td class="am-hide-sm-only" ><div _switch="" class="am-switch am-round am-switch-success changelook  newDisplay am-active"><div class="am-switch-handle"><input type="checkbox" class="" ' + (dataContent[i].Look == 0 ? "checked='true'" : "") + ' data-id="' + dataContent[i].id + '"></div></div></td>';
                    showHtml += '<td>' + dataContent[i].Author + '</td>';
                    showHtml += '<td class="am-hide-sm-only">' + dataContent[i].Creator + '</td>';
                    showHtml += '<td class="am-hide-sm-only">' + dataContent[i].CreateTime + '</td>';
                    showHtml += '<td><div class="am-btn-toolbar"><div class="am-btn-group am-btn-group-xs">';
                    showHtml += '<a class="am-btn am-btn-default am-btn-xs am-text-secondary edit-model" data-id="' + dataContent[i].id + '"><span class="am-icon-pencil-square-o"></span> 编辑</a>';
                    showHtml += '<a class="am-btn am-btn-default am-btn-xs am-text-danger am-hide-sm-only" onclick="Del(@item.id)"><span class="am-icon-trash-o"></span> 删除</a>';
                    showHtml += '</div></div></td></tr>';
                    //-----------------
                    //showHtml += '<tr>';
                    //showHtml +='<td>'+ (i + 1) + '</td>';
                    //showHtml +='<td>' + dataContent[i].Title +'</td>';
                    //showHtml +='<td class="am-hide-sm-only" data-id="'+dataContent[i].id+'"><div _switch="" class="am-switch am-round am-switch-success  newDisplay am-active"><div class="am-switch-handle"><input type="checkbox" class="" checked=""></div></div></td>';
                    //showHtml +='<td>' + dataContent[i].Author + '</td>';
                    //showHtml += '<td class="am-hide-sm-only">' + dataContent[i].Creator + '</td>';
                    //showHtml += '<td class="am-hide-sm-only">' + dataContent[i].CreateTime + '</td>';
                    //showHtml += '<td><div class="am-btn-toolbar"><div class="am-btn-group am-btn-group-xs">';
                    //showHtml += '<a class="am-btn am-btn-default am-btn-xs am-text-secondary" id="btnEdit"><span class="am-icon-pencil-square-o"></span> 编辑</a>';
                    //showHtml += '<a class="am-btn am-btn-default am-btn-xs am-text-danger am-hide-sm-only" onclick="Del(1)"><span class="am-icon-trash-o"></span> 删除</a>';
                    //showHtml += '</div></div></td></tr>';
                }
                $("#showTb").html(showHtml);
            }
        }, "json")

    }


    /*******************************
    删除
    *******************************/
    function Del(mId) {
        if (mId == null) {
            bfeMsgBox.error("", "数据异常，请刷新重试");
            return;
        }
        $.ajax({
            url: "/admin/manage/Del",
            data: { "id": mId },
            type: "post",
            dataType: "json",
            success: function (e) {
                if (e.status == 0) {
                    window.location.reload();
                }
                else {
                    bfeMsgBox.error("", e.msg);
                }
            },
            error: function (er) {
                bfeMsgBox.error("", er);
            }
        })
    }

