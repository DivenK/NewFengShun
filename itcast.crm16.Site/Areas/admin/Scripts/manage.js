var addFlag = 0;
var editId = 0;
var pageIndex = 1;
var pageSize = 15;
var $modal;
var isFirst = true;


$(function () {
    //控制新增的弹层
    $modal = $('#doc-modal-1');//弹窗div  
    SetPageHtml();
    isFirst = false;

    $('#btnAddManage').on('click', function (e) {
        //修改新增标识
        addFlag = 0;
        $("#doc-title").val("");
        $("#doc-author").val("");
        editor.setContent("");
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
            "id": editId,
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
        editId = 0;
        $modal.modal('close');
    })


    /****************************************
    3.0 搜索查询数据
    ****************************************/
    $("#btnSearch").unbind("click").bind("click", function () {
        SetPageHtml();
    })


})
/****************************************
4.0 开启或者关闭评论功能
****************************************/
$(document).on('click', '.changelook', function () {
    var content = {
        "id": $(this).find("input[type=checkbox]").attr("data-id"),
        "look": ($(this).hasClass('am-active') ? 1 : 0)
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
    var content = {
        "id": $(this).attr("data-id")
    }
    $.ajax({
        url: "/admin/manage/GetManageById",
        data: content,
        type: "get",
        datatype: "json",
        success: function (e) {
            if (e.status == 0) {
                addFlag = 1;
                editId = e.datas.id;
                $("#doc-title").val(e.datas.Title);
                $("#doc-author").val(e.datas.Author);
                editor.setContent(e.datas.Conter);
                $modal.modal({ closeViaDimmer: 0, width: 800, height: 800 });//弹起
            }
            else {
                bfeMsgBox.error("", e.msg);
            }
        },
        error: function (er) {
            bfeMsgBox.error("", er);
            editId = 0;
        }
    })
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
                showHtml += '<td class="am-hide-sm-only" ><div _switch="" class="am-switch am-round am-switch-success changelook  newDisplay '+ (dataContent[i].Look == 0 ? "am-active" : "") + '"><div class="am-switch-handle"><input type="checkbox" class="" ' + (dataContent[i].Look == 0 ? "checked" : "") + ' data-id="' + dataContent[i].id + '"></div></div></td>';
                showHtml += '<td>' + dataContent[i].Author + '</td>';
                showHtml += '<td class="am-hide-sm-only">' + dataContent[i].Creator + '</td>';
                showHtml += '<td class="am-hide-sm-only">' + dataContent[i].CreateTime + '</td>';
                showHtml += '<td><div class="am-btn-toolbar"><div class="am-btn-group am-btn-group-xs">';
                showHtml += '<a class="am-btn am-btn-default am-btn-xs am-text-secondary edit-model" data-id="' + dataContent[i].id + '"><span class="am-icon-pencil-square-o"></span> 编辑</a>';
                showHtml += '<a class="am-btn am-btn-default am-btn-xs am-text-danger am-hide-sm-only del-model" data-id="' + dataContent[i].id + '"><span class="am-icon-trash-o"></span> 删除</a>';
                showHtml += '</div></div></td></tr>';
            }
            $("#showTb").html(showHtml);
        }
    }, "json")

}


/*******************************
删除
*******************************/
$(document).on("click", ".del-model", function () {
    var mid = $(this).attr("data-id");
    $('#my-confirm-Del').modal({
        relatedTarget: this,
        onConfirm: function (options) {
            $.ajax({
                url: "/admin/manage/Del",
                data: { "id": mid },
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
        },
        // closeOnConfirm: false,
        onCancel: function () {

        }
    });


})



//赋值页面上的分页
function SetPageHtml() {
    var pageCount = $("#pageDemo").attr("data-PageCount");
    var pageIndex = $("#pageDemo").attr("data-indexPage");
    var searchContent = $("#txtSearchContent").val();
    $('#pageDemo').page({
        pages: pageCount,
        first: "首页", //设置false则不显示，默认为false  
        last: "尾页", //设置false则不显示，默认为false      
        prev: '<', //若不显示，设置false即可，默认为上一页
        next: '>', //若不显示，设置false即可，默认为下一页
        groups: 5, //连续显示分页数
        jump: function (context) {
            if (!isFirst) {
                GetContentByPage(context.option.curr, 10, searchContent);
            }
        }//这里就是去异步请求方法
    });
}

