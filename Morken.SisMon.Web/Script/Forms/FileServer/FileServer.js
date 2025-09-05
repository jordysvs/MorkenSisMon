jQuery.extend({

    createUploadIframe: function (id, uri) {
        //create frame
        var frameId = 'jUploadFrame' + id;
        var iframeHtml = '<iframe id="' + frameId + '" name="' + frameId + '" style="position:absolute; top:-9999px; left:-9999px"';
        if (window.ActiveXObject) {
            if (typeof uri == 'boolean') {
                iframeHtml += ' src="' + 'javascript:false' + '"';

            }
            else if (typeof uri == 'string') {
                iframeHtml += ' src="' + uri + '"';

            }
        }
        iframeHtml += ' />';
        jQuery(iframeHtml).appendTo(document.body);

        return jQuery('#' + frameId).get(0);
    },

    createUploadForm: function (id, fileElementId, data) {
        //create form	
        var formId = 'jUploadForm' + id;
        var fileId = 'jUploadFile' + id;
        var form = jQuery('<form  action="" method="POST" name="' + formId + '" id="' + formId + '" enctype="multipart/form-data"></form>');
        if (data) {
            for (var i in data) {
                jQuery('<input type="hidden" name="' + i + '" value="' + data[i] + '" />').appendTo(form);
            }
        }
        var oldElement = jQuery('#' + fileElementId);
        var newElement = jQuery(oldElement).clone();
        jQuery(oldElement).attr('id', fileId);
        jQuery(oldElement).before(newElement);
        jQuery(oldElement).appendTo(form);

        //set attributes
        jQuery(form).css('position', 'absolute');
        jQuery(form).css('top', '-1200px');
        jQuery(form).css('left', '-1200px');
        jQuery(form).appendTo('body');
        return form;
    },

    ajaxFileUpload: function (s) {
        // TODO introduce global settings, allowing the client to modify them for all requests, not only timeout		
        s = jQuery.extend({}, jQuery.ajaxSettings, s);
        var id = new Date().getTime()
        var form = jQuery.createUploadForm(id, s.fileElementId, (typeof (s.data) == 'undefined' ? false : s.data));
        var io = jQuery.createUploadIframe(id, s.secureuri);
        var frameId = 'jUploadFrame' + id;
        var formId = 'jUploadForm' + id;
        // Watch for a new set of requests
        if (s.global && !jQuery.active++) {
            jQuery.event.trigger("ajaxStart");
        }
        var requestDone = false;
        // Create the request object
        var xml = {}
        if (s.global)
            jQuery.event.trigger("ajaxSend", [xml, s]);
        // Wait for a response to come back
        var uploadCallback = function (isTimeout) {
            var io = document.getElementById(frameId);
            try {
                if (io.contentWindow) {
                    xml.responseText = io.contentWindow.document.body ? io.contentWindow.document.body.innerHTML : null;
                    xml.responseXML = io.contentWindow.document.XMLDocument ? io.contentWindow.document.XMLDocument : io.contentWindow.document;

                } else if (io.contentDocument) {
                    xml.responseText = io.contentDocument.document.body ? io.contentDocument.document.body.innerHTML : null;
                    xml.responseXML = io.contentDocument.document.XMLDocument ? io.contentDocument.document.XMLDocument : io.contentDocument.document;
                }
            } catch (e) {
                jQuery.handleError(s, xml, null, e);
            }
            if (xml || isTimeout == "timeout") {
                requestDone = true;
                var status;
                try {
                    status = isTimeout != "timeout" ? "success" : "error";
                    // Make sure that the request was successful or notmodified
                    if (status != "error") {
                        // process the data (runs the xml through httpData regardless of callback)
                        var data = jQuery.uploadHttpData(xml, s.dataType);
                        // If a local callback was specified, fire it and pass it the data
                        if (s.success)
                            s.success(data, status);

                        // Fire the global callback
                        if (s.global)
                            jQuery.event.trigger("ajaxSuccess", [xml, s]);
                    } else
                        jQuery.handleError(s, xml, status);
                } catch (e) {
                    status = "error";
                    jQuery.handleError(s, xml, status, e);
                }

                // The request was completed
                if (s.global)
                    jQuery.event.trigger("ajaxComplete", [xml, s]);

                // Handle the global AJAX counter
                if (s.global && ! --jQuery.active)
                    jQuery.event.trigger("ajaxStop");

                // Process result
                if (s.complete)
                    s.complete(xml, status);

                jQuery(io).unbind()

                setTimeout(function () {
                    try {
                        jQuery(io).remove();
                        jQuery(form).remove();

                    } catch (e) {
                        jQuery.handleError(s, xml, null, e);
                    }

                }, 100)

                xml = null

            }
        }
        // Timeout checker
        if (s.timeout > 0) {
            setTimeout(function () {
                // Check to see if the request is still happening
                if (!requestDone) uploadCallback("timeout");
            }, s.timeout);
        }
        try {

            var form = jQuery('#' + formId);
            jQuery(form).attr('action', s.url);
            jQuery(form).attr('method', 'POST');
            jQuery(form).attr('target', frameId);
            if (form.encoding) {
                jQuery(form).attr('encoding', 'multipart/form-data');
            }
            else {
                jQuery(form).attr('enctype', 'multipart/form-data');
            }
            jQuery(form).submit();

        } catch (e) {
            jQuery.handleError(s, xml, null, e);
        }

        jQuery('#' + frameId).load(uploadCallback);
        return { abort: function () { } };

    },

    uploadHttpData: function (r, type) {
        var data = !type;
        data = type == "xml" || data ? r.responseXML : r.responseText;
        // If the type is "script", eval it in global context
        if (type == "script")
            jQuery.globalEval(data);
        // Get the JavaScript object, if JSON is used.
        if (type == "json")
            eval("data = " + data);
        // evaluate scripts within html
        if (type == "html")
            jQuery("<div>").html(data).evalScripts();

        return data;
    },

    handleError: function (s, xhr, status, e) {
        // If a local callback was specified, fire it
        if (s.error) {
            s.error.call(s.context || window, xhr, status, e);
        }

        // Fire the global callback
        if (s.global) {
            (s.context ? jQuery(s.context) : jQuery.event).trigger("ajaxError", [xhr, s, e]);
        }
    }
});


(function ($) {
    var methods = {
        init: function (options) {
            return this.each(function () {
                var $this = $(this),
                    data = $this.data('fileserver');

                if (!data) {
                    $(this).data('fileserver', {
                        url: '../../Ashx/FileServerHandler.ashx',
                        idDocumento: null,
                        idAlmacen: null,
                        idRegistro: null,
                        successMessage: true,
                        extension: '',
                        replace: false,
                        resize: false,
                        width: null,
                        height: null,
                        success: function () { },
                        error: function () { }
                    });
                    data = $this.data('fileserver');

                    if (options.autoUpload) {
                        $(this).unbind("change");
                        $(this).on("change", function () {
                            $(this).fileserver('Upload');
                        });
                    }
                }
                if (options != undefined)
                    $this.data('fileserver', $.extend($this.data('fileserver'), options));
            });
        },

        Upload: function () {

            var id = this.attr('id');
            var arg = $(this).data('fileserver');

            //validacion de existencia del archivo

            var files = document.getElementById(id).files.length;
            if (files <= 0) {
                AlertJQ(1, 'Seleccione un archivo.');
                return false;
            }

            //validacion de extension del archivo
            var blnExito = true;
            var validExtensions = [];
            var str_filePath = this.val();
            if (arg.extension != null && arg.extension != '') {
                blnExito = false;
                var ext = str_filePath.substring(str_filePath.lastIndexOf('.') + 1).toLowerCase();
                validExtensions = arg.extension.split(',');
                for (var i = 0; i < validExtensions.length; i++) {
                    if (ext == validExtensions[i].toLowerCase()) {
                        blnExito = true;
                        break;
                    }
                }
            }

            if (!blnExito) {
                AlertJQ(4, 'La extensión de archivo ' + ext.toUpperCase() + ' no está permitido');
                return false;
            }

            var input = {
                int_pIdAlmacen: arg.idAlmacen,
                int_pIdRegistro: arg.idRegistro,
                str_pDescripcion: arg.descripcion,
                bln_pReplace: arg.replace,
                bln_pResize: arg.resize,
                int_pWidth: arg.width,
                int_pHeight: arg.height
            };

            MostrarMensajeCargando();

            $.ajaxFileUpload
            (
                {
                    url: arg.url,
                    secureuri: false,
                    fileElementId: this.attr('name'),
                    dataType: 'json',
                    data: input,
                    success: function (result, status) {
                        $('#' + id).fileserver(arg);
                        if (result.hasOwnProperty('OperationResult')) {
                            if (result.OperationResult == 1)
                                if (arg.success != null || arg.success != undefined)
                                    arg.success.apply(this, [result]);
                        }
                        CerrarMensajeCargando();
                        if (arg.successMessage)
                            if (result.hasOwnProperty('Message'))
                                AlertJQ(2, result.Message);
                    },
                    error: function (result, status, e) {
                        $('#' + id).fileserver(arg);
                        if (arg.error != null)
                            arg.error.apply(this);
                        CerrarMensajeCargando();
                        AlertJQ(4, e);
                    }
                }
            )
        }
    };

    $.fn.fileserver = function (method) {
        if (methods[method]) {
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            return methods.init.apply(this, arguments);
        } else {
            $.error('Method ' + method + ' does not exist on jQuery.fileserver');
        }
    };

})(jQuery);






