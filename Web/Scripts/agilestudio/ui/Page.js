function Page(options) {
    var self = this;
    options = options || {};

    self.active = options.active;
    self.page = options.page;
    self.limit = options.limit;
}