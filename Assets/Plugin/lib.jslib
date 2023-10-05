mergeInto(LibraryManager.library, {
  ReloadPage: function() {
    location.reload();
  },

  CopyTextToClipboard: function(textPtr) {
    var text = UTF8ToString(textPtr);
    navigator.clipboard.writeText(text).catch(function(err) {
      console.error('Failed to copy text: ', err);
    });
  }
});
