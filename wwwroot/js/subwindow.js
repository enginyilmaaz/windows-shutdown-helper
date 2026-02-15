// Sub Window - renders a single page based on URL query parameter
(function () {
    var params = new URLSearchParams(window.location.search);
    var pageName = params.get('page') || 'main';

    var pages = {
        main: MainPage,
        settings: SettingsPage,
        logs: LogsPage
    };

    function applyLanguage() {
        var L = Bridge.lang.bind(Bridge);
        var title = document.getElementById('header-title');
        if (!title) return;

        if (pageName === 'main') {
            title.textContent = L('main_groupbox_newAction') || 'Actions';
        } else if (pageName === 'settings') {
            title.textContent = L('settingsForm_Name') || 'Settings';
        } else if (pageName === 'logs') {
            title.textContent = L('logViewerForm_Name') || 'Logs';
        } else {
            title.textContent = L('main_FormName') || 'Windows Shutdown Helper';
        }
    }

    function renderPage() {
        var page = pages[pageName];
        if (!page) return;

        var container = document.getElementById('page-container');
        container.innerHTML = page.render();

        if (page.afterRender) {
            page.afterRender();
        }
    }

    // Time update
    Bridge.on('updateTime', function (data) {
        var el = document.getElementById('header-time');
        if (el) el.textContent = data;
    });

    // Wait for init from C#
    Bridge.on('init', function () {
        applyLanguage();
        renderPage();
    });

    // Listen for navigate messages (for re-render if needed)
    Bridge.on('navigate', function (page) {
        if (pages[page]) {
            pageName = page;
            applyLanguage();
            renderPage();
        }
    });
})();
