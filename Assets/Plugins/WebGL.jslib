var OpenWindowPlugin = {
    openWindow: function(link)
    {
    	var url = Pointer_stringify(link);
        window.open(url);
    }
};

var IsMobilePlugin = {
    IsMobile: function () {
        var ua = window.navigator.userAgent.toLowerCase();
        var mobilePattern = /android|iphone|ipad|ipod/i;

        return ua.search(mobilePattern) !== -1 || (ua.indexOf("macintosh") !== -1 && "ontouchend" in document);
    }
};

mergeInto(LibraryManager.library, OpenWindowPlugin);
mergeInto(LibraryManager.library, IsMobilePlugin);