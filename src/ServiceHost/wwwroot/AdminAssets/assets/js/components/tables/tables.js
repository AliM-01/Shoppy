(function ($) {

	'use strict';

	// ------------------------------------------------------- //
	// Auto Hide
	// ------------------------------------------------------ //	

    $(function () {
		$('#jquery-data-table').DataTable({
			dom: 'Bfrtip',
			buttons: {
				buttons: [{
					extend: 'copy',
					text: 'کپی',
					title: $('h1').text(),
					exportOptions: {
						columns: ':not(.no-print)'
					},
					footer: true
				},{
					extend: 'excel',
					text: 'فایل Excel',
					title: $('h1').text(),
					exportOptions: {
						columns: ':not(.no-print)'
					},
					footer: true
				},{
					extend: 'csv',
					text: 'فایل Csv',
					title: $('h1').text(),
					exportOptions: {
						columns: ':not(.no-print)'
					},
					footer: true
				},{
					extend: 'pdf',
					text: 'فایل Pdf',
					title: $('h1').text(),
					exportOptions: {
						columns: ':not(.no-print)'
					},
					footer: true
				},{
					extend: 'print',
					text: 'پرینت',
					title: $('h1').text(),
					exportOptions: {
						columns: ':not(.no-print)'
					},
					footer: true,
					autoPrint: false
				}],
				dom: {
					container: {
						className: 'dt-buttons mb-2'
					},
					button: {
						className: 'btn btn-primary'
					}
				}
			}
		});
	});

})(jQuery);