// Help Page - Trigger usage guide
window.HelpPage = {
    _isTurkish() {
        var selected = Bridge && Bridge._settings ? Bridge._settings.language : '';
        if (!selected || selected === 'auto') {
            selected = (navigator.language || 'en');
        }

        return String(selected).toLowerCase().indexOf('tr') === 0;
    },

    _copy() {
        if (this._isTurkish()) {
            return {
                intro: 'Bu ekranda tetikleyicilerin ne zaman calistigini ve menuleri nasil kullanacaginizi bulabilirsiniz.',
                quickTitle: 'Hizli Baslangic',
                quick1: '1) Yeni Gorev butonuna basin.',
                quick2: '2) Gorev Turu ve Tetikleyici secin.',
                quick3: '3) Tetikleyiciye gore degeri girin ve gorevi kaydedin.',
                triggerTitle: 'Tetikleyiciler',
                triggerFromNow: 'Geriye Sayim: Verilen sure doldugunda bir kez calisir.',
                triggerIdle: 'Bosta kalinan sure: Klavye/fare kullanimi olmadiginda calisir.',
                triggerCertainTime: 'Saate gore her gun: Her gun belirlenen saatte bir kez calisir.',
                triggerBluetooth: 'Bluetooth Kilidi: Secilen cihaz once gorulur, bagli iken baglantisi kesildiginde kilitler. Cihaz tekrar baglanip tekrar koparsa yeniden kilitler.',
                bluetoothNoteTitle: 'Bluetooth Kilidi Notlari',
                bluetoothNote1: 'Ayarlar ekranindaki "Bluetooth esik suresi (sn)" degeri, cihazin koptu sayilmasi icin beklenen suredir.',
                bluetoothNote2: 'Ilk tetikleme icin cihazin en az bir kez algilanmasi gerekir.',
                menuTitle: 'Menuler',
                menu1: 'Hamburger menu: Ayarlar, Kayitlar, Yardim ve Hakkinda pencerelerini acar.',
                menu2: 'Aksiyon listesinde sag tik menu: Secili gorevi siler, tumunu temizler veya yardimi acar.',
                tipsTitle: 'Ek Ipuclari',
                tip1: 'Geriye Sayim tetikleyicisi calistiktan sonra listeden otomatik kaldirilir.',
                tip2: 'Saate gore her gun tetikleyicisi her gun yalnizca bir kez calisir.',
                tip3: 'Ayarlarda duraklat/devam ettir ile tum gorevleri gecici olarak kontrol edebilirsiniz.'
            };
        }

        return {
            intro: 'This page explains when each trigger runs and how to use the menus.',
            quickTitle: 'Quick Start',
            quick1: '1) Click New Action.',
            quick2: '2) Choose Action Type and Trigger.',
            quick3: '3) Enter trigger value and save.',
            triggerTitle: 'Triggers',
            triggerFromNow: 'Countdown: Runs once when the specified duration ends.',
            triggerIdle: 'System Idle: Runs when there is no keyboard or mouse input for the selected duration.',
            triggerCertainTime: 'Every day by time: Runs once per day at the selected time.',
            triggerBluetooth: 'Bluetooth Lock: The selected device must be seen first. When it disconnects after being reachable, the lock action runs. If it reconnects and disconnects again, it runs again.',
            bluetoothNoteTitle: 'Bluetooth Lock Notes',
            bluetoothNote1: 'The "Bluetooth threshold (sec)" setting controls how long to wait before treating the device as disconnected.',
            bluetoothNote2: 'The first trigger requires the device to be detected at least once.',
            menuTitle: 'Menus',
            menu1: 'Hamburger menu opens Settings, Logs, Help, and About windows.',
            menu2: 'Right-click menu on the action list can delete selected item, clear all actions, or open this help page.',
            tipsTitle: 'Extra Tips',
            tip1: 'Countdown actions are removed automatically after execution.',
            tip2: 'Daily time trigger runs only once per day.',
            tip3: 'Use pause/resume in Settings to temporarily stop all actions.'
        };
    },

    render() {
        var L = Bridge.lang.bind(Bridge);
        var t = this._copy();

        return '' +
        '<div class="card">' +
            '<div class="card-title">' +
                '<span class="mi">help</span>' +
                (L('HelpMenuItem') || 'Help') +
            '</div>' +
            '<div class="help-content">' +
                '<div class="help-intro">' + t.intro + '</div>' +
                '<div class="help-section">' +
                    '<div class="help-section-title">' + t.quickTitle + '</div>' +
                    '<div class="help-line">' + t.quick1 + '</div>' +
                    '<div class="help-line">' + t.quick2 + '</div>' +
                    '<div class="help-line">' + t.quick3 + '</div>' +
                '</div>' +
                '<div class="help-section">' +
                    '<div class="help-section-title">' + t.triggerTitle + '</div>' +
                    '<div class="help-line">' + t.triggerFromNow + '</div>' +
                    '<div class="help-line">' + t.triggerIdle + '</div>' +
                    '<div class="help-line">' + t.triggerCertainTime + '</div>' +
                    '<div class="help-line">' + t.triggerBluetooth + '</div>' +
                '</div>' +
                '<div class="help-section">' +
                    '<div class="help-section-title">' + t.bluetoothNoteTitle + '</div>' +
                    '<div class="help-line">' + t.bluetoothNote1 + '</div>' +
                    '<div class="help-line">' + t.bluetoothNote2 + '</div>' +
                '</div>' +
                '<div class="help-section">' +
                    '<div class="help-section-title">' + t.menuTitle + '</div>' +
                    '<div class="help-line">' + t.menu1 + '</div>' +
                    '<div class="help-line">' + t.menu2 + '</div>' +
                '</div>' +
                '<div class="help-section">' +
                    '<div class="help-section-title">' + t.tipsTitle + '</div>' +
                    '<div class="help-line">' + t.tip1 + '</div>' +
                    '<div class="help-line">' + t.tip2 + '</div>' +
                    '<div class="help-line">' + t.tip3 + '</div>' +
                '</div>' +
            '</div>' +
        '</div>';
    }
};
