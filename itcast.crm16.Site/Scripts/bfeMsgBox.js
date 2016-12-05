/*
 * BFE-MessageBox
 * Copyright 2014-2015
 * Authors: bushidian
 */
/* global define */

(function (define) {
    define(['jquery'], function ($) {
        return (function () {
         
            var bfeMsgBoxType = {
                error: 'error',
                info: 'info',
                success: 'success',
                warning: 'warning',
                confirm: 'confirm'
            };

            function error(content,title) {
                content = content.length > 200 ? content.substring(0, 200) + '......' : content;
                
                    swal({title:'错误', content : '操作失败',type: bfeMsgBoxType.info,timer:2000});
            }

            function info(content,title) {
          
                  swal({title:'提示', content : '提示',type: bfeMsgBoxType.info,timer:2000});
            }

            function success(title, content) {
                swal({title:'成功', content : '成功',type: bfeMsgBoxType.success,timer:2000});
            }

            function confirm(content, callback, title) {
                swal({
                    title: title || '您确定',
                    text: content,
                    type: bfeMsgBoxType.warninge,
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Yes",
                    cancelButtonText: "No",
                    closeOnConfirm: false,
                    closeOnCancel: false
                }, function(isConfirm) {
                    if (isConfirm && typeof (callback) == 'function') {
                        callback(isConfirm);
                    }
                    swal.close();
                });
            }

            function warning(content, title) {

                toastr.warning(content, title);
            }

            ////////////////

            var bfeMsgBox = {
                info: info,
                error: error,
                warning: warning,
                confirm: confirm,
                success: success,
                version: '1.1'
            };

            return bfeMsgBox;

         
        })();
    });
}(typeof define === 'function' && define.amd ? define : function (deps, factory) {
    if (typeof module !== 'undefined' && module.exports) { //Node
        module.exports = factory(require('jquery'));
    } else {
        window['bfeMsgBox'] = factory(window['jQuery']);
    }
}));

