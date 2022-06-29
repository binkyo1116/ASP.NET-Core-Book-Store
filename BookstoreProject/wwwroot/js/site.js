function checkSearchForm() {
    var _in = $('#txtSearchItem').val();
    if (_in == null || _in == "") {
        alert("Arama kismi bos birakilamaz."); // buraya sweetalert yada notification gelicek.
        return false;
    }
    document.getElementById('searchForm').submit();
    return true;
}