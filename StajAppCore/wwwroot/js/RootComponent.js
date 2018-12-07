var HostApp = new Vue({
    el: '#App',
    data: {
        currentView: 'tab1'
    },
    methods: {
        swichView: function (view) {
            this.currentView = view;
        }
    }
});