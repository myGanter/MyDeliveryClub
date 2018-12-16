var HostApp = new Vue({
    el: '#App',
    data: {
        currentView: 'tab1',
        msg: {},
        onDisplay: false,
        logDisplay: true
    },
    //created() {
        
    //},
    methods: {
        swichView: function (view) {
            this.currentView = view;
        },
        showMsg: function (mes) {
            this.msg = mes;
            this.onDisplay = true;
            console.log(this.msg);
        }
    }
});