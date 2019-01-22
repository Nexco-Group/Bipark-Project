/*!
 * remark (https://getbootstrapadmin.com/remark)
 * Copyright 2017 amazingsurge
 * Licensed under the Themeforest Standard Licenses
 */

//function changesearchnew2() {
//    var money1 = 0;
//    var gig1 = 0;
//    $("#divloader").show();
//    setTimeout(function () {
//        $('#foo7 > tbody  > tr').each(function () {
//            if ($(this).find("td:nth-child(11)") != null) {
//                gig1 += parseFloat($(this).find("td:nth-child(11)").html());
//                money1 += parseInt($(this).find("td:nth-child(12)").html());
//            }
//        });

//        $("#foo7o").html('<td>کل فروش: ' + money1.toLocaleString() + ' ریال</td><td>کل حجم فروخته شده: ' + gig1 + ' گیگابایت</td>');
//        $("#divloader").hide();
//    }, 1500);
//}
(function (document, window, $) {
    'use strict';

    var Site = window.Site;

    $(document).ready(function ($) {
        Site.run();
    });

    // Example Row Toggler
    // -------------------
    (function () {
        $('#exampleRowToggler').footable({
            "toggleColumn": "first",
            "showToggle": true,
            "expandFirst": true
        });
    })();

    // Accordion
    // ---------
    (function () {
        $('#exampleFooAccordion').footable();
    })();

    // Collapse
    // --------------------------
    (function () {
        $('#exampleFooCollapse').footable();
    })();

    // NO HEADERS
    // ----------
    (function () {
        $('#exampleNoHeaders').footable();
    })();

    // Pagination
    // ----------
    (function () {
        $('#examplePagination').footable();
        $('#exampleShow [data-page-size]').on('click', function (e) {
            e.preventDefault();
            var pagesize = $(this).data('pageSize');
            FooTable.get('#examplePagination').pageSize(pagesize);
        });
    })();

    // Custom filter UI
    // ----------
    (function () {
        $('#exampleCustomFilter').footable();
        $('.filter-ui-status').on('change', function () {
            var filtering = FooTable.get('#exampleCustomFilter').use(FooTable.Filtering), // get the filtering component for the table
                filter = $(this).val(); // get the value to filter by
            if (filter === 'none') { // if the value is "none" remove the filter
                filtering.removeFilter('status');
            } else { // otherwise add/update the filter.
                filtering.addFilter('status', filter, ['status']);
            }
            filtering.filter();
        });
    })();

    // Modal
    // ----------
    (function () {
        $('#exampleModal').footable({
            "useParentWidth": true
        });
    })();

    // Loading Rows
    // ----------
    (function () {
        $('#exampleLoading').footable();
        var loading = FooTable.get('#exampleLoading');

        $('.append-rows').on('click', function (e) {
            e.preventDefault();
            // get the url to load off the button
            var url = $(this).data('url');
            // ajax fetch the rows
            $.get(url).then(function (rows) {
                // and then load them using either
                loading.rows.load(rows);
                // or
                // ft.loadRows(rows);
            });
        });
    })();

    // Filtering
    // ---------
    (function () {
        FooTable.MyFiltering = FooTable.Filtering.extend({
            construct: function (instance) {
                this._super(instance);
                this.statuses = ['تایید شده', 'لغو شده توسط ادمین', 'لغو شده توسط کاربر', 'ثبت شده - منتظر تایید'];
                this.def = 'انتخاب وضعیت';
                this.$status = null;
            },
            $create: function () {
                this._super();
                var self = this,
                    $form_grp = $('<div/>', {
                        'class': 'form-group'
                    })
                        .append($('<label/>', {
                            'class': 'sr-only',
                            text: 'Status'
                        }))
                        .prependTo(self.$form);

                self.$status = $('<select/>', {
                    'class': 'form-control'
                })
                    .on('change', {
                        self: self
                    }, self._onStatusDropdownChanged)
                    .append($('<option/>', {
                        text: self.def
                    }))
                    .appendTo($form_grp);

                $.each(self.statuses, function (i, status) {
                    self.$status.append($('<option/>').text(status));
                });
            },
            _onStatusDropdownChanged: function (e) {
                var self = e.data.self,
                    selected = $(this).val();
                changesearchnew2();
                if (selected !== self.def) {
                    self.addFilter('status', selected, ['status']);
                } else {
                    self.removeFilter('status');
                }
                self.filter();
            },
            draw: function () {
                this._super();
                var status = this.find('status');
                if (status instanceof FooTable.Filter) {
                    this.$status.val(status.query.val());
                } else {
                    this.$status.val(this.def);
                }
            }
        });

        //FooTable.components.register('filtering', FooTable.MyFiltering);
        //var filtering = $('#exampleFootableFiltering');
        //filtering.footable();
    })();


})(document, window, jQuery);
