jQuery( document ).ready(function( $ ) {
    "use strict"
        
        // ------- Banner Slider ------- //
        $('#banner-slider' ).sliderPro({
            width: 1920,
            height: 800,
            arrows: false,
            fade: true,
            buttons: false,
            waitForLayers: true,
            thumbnailWidth: 15,
            thumbnailHeight: 15,
            thumbnailPointer: true,
            autoplay: true,
            autoScaleLayers: false,
            rtl: true
        });
        // ------- Banner Slider ------- //
    
        // ------- slide menu ------- //
        $('.toggleMenu').bigSlide();
        // ------- slide menu ------- //
    
        // ------- Scroll Top Back ------- //
        
        // ------- Scroll Top Back ------- //
    
        // ---------- On Click Show Address ---------- //
        $("#search-open-btn").on("click",function(){
            $(".search-bar").toggleClass("open-search")();
        });
        // ---------- On Click Show Address ---------- //
    
        // ------- Parallax ------- //    
        //Plugin activation
        $(window).enllax(); 
        // $('#some-id').enllax();
        //     $('selector').enllax({
        //     type: 'background', // 'foreground'
        //     ratio: 0.5,
        //     direction: 'vertical' // 'horizontal'
        // });
        // ------- Parallax ------- //
    
        // ------- Timeline Slider ------- //
        jQuery('#timeline-slider').slick({
            dots: false,
            infinite: true,
            speed: 700,
            arrows: false,
            slidesToShow: 4,
            slidesToScroll: 1,
            rtl: true,
            responsive: [
                { breakpoint: 102, settings:{ slidesToShow: 3, slidesToScroll: 1}},
                { breakpoint: 991, settings:{ slidesToShow: 3, slidesToScroll: 1}},
                { breakpoint: 600, settings:{ slidesToShow: 2, slidesToScroll: 1}},
                { breakpoint: 480, settings:{ slidesToShow: 2, slidesToScroll: 1}}
          ]
        });
        // ------- Timeline Slider ------- //
    
        // ------- Team Slider ------- //
        jQuery('#team-slider').slick({
            dots: false,
            infinite: true,
            speed: 700,
            arrows: true,
            slidesToShow: 4,
            slidesToScroll: 1,
            rtl: true,
            responsive: [
                { breakpoint: 1024, settings:{ slidesToShow: 3, slidesToScroll: 1}},
                { breakpoint: 992, settings:{ slidesToShow: 3, slidesToScroll: 1}},
                { breakpoint: 600, settings:{ slidesToShow: 2, slidesToScroll: 1}},
                { breakpoint: 480, settings:{ slidesToShow: 1, slidesToScroll: 1}}
          ]
        });
        // ------- Team Slider ------- //
    
        // ------- Team Slider ------- //
        jQuery('#team-slider-2').slick({
            dots: false,
            infinite: true,
            speed: 700,
            arrows: false,
            slidesToShow: 3,
            slidesToScroll: 1,
            rtl: true,
            responsive: [
                { breakpoint: 1024, settings:{ slidesToShow: 3, slidesToScroll: 1}},
                { breakpoint: 992, settings:{ slidesToShow: 3, slidesToScroll: 1}},
                { breakpoint: 600, settings:{ slidesToShow: 2, slidesToScroll: 1}},
                { breakpoint: 480, settings:{ slidesToShow: 1, slidesToScroll: 1}}
          ]
        });
        // ------- Team Slider ------- //
    
        // ------- Brands Slide ------- //
        jQuery('#brands-slide').slick({
            dots: false,
            infinite: true,
            speed: 700,
            arrows: true,
            slidesToShow: 6,
            slidesToScroll: 1,
            rtl: true,
            responsive: [
                { breakpoint: 991, settings:{ slidesToShow: 4, slidesToScroll: 1}},
                { breakpoint: 641, settings:{ slidesToShow: 4, slidesToScroll: 1}},
                { breakpoint: 480, settings:{ slidesToShow: 3, slidesToScroll: 1}}
          ]
        });
        // ------- Brands Slide ------- //
    
        // ------- Statistics ------- //
        try {
            jQuery('#tc-counters').appear(function () {
                jQuery('.timer').each(count);
                function count(options) {
                    var $this = jQuery(this);
                    options = $.extend({}, options || {}, $this.data('countToOptions') || {});
                    $this.countTo(options);
                }
            });
        } catch (err) {}      
        // ------- Statistics ------- //  
    
        // ------- Timeline Slider ------- //
        jQuery('#testimonial-slider').slick({
            dots: false,
            infinite: true,
            speed: 700,
            rtl: true,
            arrows: true,
            slidesToShow: 1,
            slidesToScroll: 1
        });
        // ------- Timeline Slider ------- //  
    
        // ------- Map ------- //  
        jQuery("#map-1").gmap3({
            map:{ options:{ center:[46.578498,2.457275], zoom: 5, scrollwheel: false}},
            marker:{ values:[{address:"66000 Perpignan, France", data:"Kinder Garden", options:{icon: "assets/images/marker.png"}}],},
        });
        // ------- Map ------- //
    
        // ------- Map ------- //  
        jQuery("#map-2").gmap3({
            map:{ options:{ center:[46.578498,2.457275], zoom: 5, scrollwheel: false}},
            marker:{ values:[{address:"66000 Perpignan, France", data:"Kinder Garden", options:{icon: "assets/images/marker.png"}}],},
        });
        // ------- Map ------- //
    
        // ------- Product Slides ------- //
        jQuery('#product-slides').slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            rtl: true,
            arrows: true,
            asNavFor: '#product-thumnail'
        });
        jQuery('#product-thumnail').slick({
            slidesToShow: 4,
            slidesToScroll: 1,
            asNavFor: '#product-slides',
            dots: false,
            focusOnSelect: true,
            arrows: false,
            rtl: true,
            responsive: [
                { breakpoint: 641, settings: { slidesToShow: 5, slidesToScroll: 1}},
                { breakpoint: 481, settings: { slidesToShow: 4, slidesToScroll: 1}},
                { breakpoint: 479, settings: { slidesToShow: 3, slidesToScroll: 1}}
            ]
        });
        // ------- Product Slides ------- //
    
        // ------- Rating Stras ------- //
        $("#product-rating").rating({
            starCaptions: function(val) {
                if (val < 3) {
                    return val;
                } else {
                    return 'high';
                }
            },
            starCaptionClasses: function(val) {
                if (val < 3) {
                    return 'label label-danger';
                } else {
                    return 'label label-success';
                }
            },
            hoverOnClear: false
        });
        // ------- Rating Stras ------- //
    
        // ------- Post Slider ------- //
        jQuery('#slider-post').slick({
            dots: false,
            infinite: true,
            speed: 700,
            fade: true,
            arrows: true,
            rtl: true,
            slidesToShow: 1,
            slidesToScroll: 1
        });
        // ------- Post Slider ------- //  
    
        // ------- Post Slider ------- //
        jQuery('#blog-center-slider').slick({
            dots: false,
            infinite: true,
            speed: 700,
            centerMode: true,
            focusOnSelect: true,
            arrows: false,
            rtl: true,
            slidesToShow: 1,
            slidesToScroll: 3
        });
        // ------- Post Slider ------- //  
    
        // ------- Gallery Slides ------- //
        jQuery('#gallery-slides').slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            arrows: false,
            rtl: true,
            asNavFor: '#gallery-thumnail'
        });
        jQuery('#gallery-thumnail').slick({
            slidesToShow: 6,
            slidesToScroll: 1,
            asNavFor: '#gallery-slides',
            dots: false,
            focusOnSelect: true,
            rtl: true,
            arrows: false,
            responsive: [
                { breakpoint: 641, settings: { slidesToShow: 5, slidesToScroll: 1}},
                { breakpoint: 481, settings: { slidesToShow: 3, slidesToScroll: 1}}
            ]
        });
        // ------- Gallery Slides ------- //
    
        // ------- News Letter Slider ------- //
        jQuery('#news-letter-slider').slick({
            dots: false,
            infinite: true,
            rtl: true,
            speed: 700,
            arrows: false,
            slidesToShow: 3,
            slidesToScroll: 1,
            responsive: [
                { breakpoint: 992, settings: { slidesToShow: 2, slidesToScroll: 1}},
                { breakpoint: 481, settings: { slidesToShow: 1, slidesToScroll: 1}}
            ]
        });
        // ------- News Letter Slider ------- //  
    
        // ------- News Letter Slider ------- //
        jQuery('#blog-grid-slider').slick({
            dots: false,
            infinite: true,
            speed: 700,
            rtl: true,
            arrows: false,
            slidesToShow: 4,
            slidesToScroll: 1,
            responsive: [
                { breakpoint: 992, settings: { slidesToShow: 2, slidesToScroll: 1}},
                { breakpoint: 641, settings: { slidesToShow: 2, slidesToScroll: 1}},
                { breakpoint: 480, settings: { slidesToShow: 1, slidesToScroll: 1}}
            ]
        });
        // ------- News Letter Slider ------- //
    
        // ------- News Letter Slider ------- //
        jQuery('#services-slider').slick({
            dots: false,
            infinite: true,
            speed: 700,
            rtl: true,
            arrows: false,
            slidesToShow: 3,
            slidesToScroll: 1,
            responsive: [
                { breakpoint: 992, settings: { slidesToShow: 3, slidesToScroll: 1}},
                { breakpoint: 641, settings: { slidesToShow: 2, slidesToScroll: 1}},
                { breakpoint: 481, settings: { slidesToShow: 2, slidesToScroll: 1}},
                { breakpoint: 480, settings: { slidesToShow: 1, slidesToScroll: 1}}
            ]
        });
        // ------- News Letter Slider ------- //  
    
        // ------- News Letter Slider ------- //
        jQuery('#weekly-classes-slider').slick({
            dots: false,
            infinite: true,
            speed: 700,
            rtl: true,
            arrows: false,
            slidesToShow: 3,
            slidesToScroll: 1,
            responsive: [
                { breakpoint: 992, settings: { slidesToShow: 2, slidesToScroll: 1}},
                { breakpoint: 641, settings: { slidesToShow: 2, slidesToScroll: 1}},
                { breakpoint: 480, settings: { slidesToShow: 1, slidesToScroll: 1}}
            ]
        });
        // ------- News Letter Slider ------- //  
    
        // ------- Scroll Top Back ------- //
        var offset = 300,
            offset_opacity = 1200,
            scroll_top_duration = 700,
            $back_to_top = $('.scrollup');
        $(window).scroll(function(){
            ( $(this).scrollTop() > offset ) ? $back_to_top.addClass('cd-is-visible') : $back_to_top.removeClass('cd-is-visible cd-fade-out');
            if( $(this).scrollTop() > offset_opacity ) { 
                $back_to_top.addClass('cd-fade-out');
            }
        });
        $back_to_top.on('click', function(event){
            event.preventDefault();
            $('body,html').animate({
                scrollTop: 0 ,
                }, scroll_top_duration
            );
        });
        // ------- Scroll Top Back ------- //
    
        // ------- Time Counter ------- //
        jQuery('#countdown-1').countdown({
            date: '11/11/2017 17:40:00',
            offset: -8352,
            day: 'Day',
            days: 'Days'
        });
        // ------- Time Counter ------- //
    
        // ---------- Wow Animation ---------- //
        var wow = new WOW({
            boxClass: 'animate',
            animateClass: 'animated',
            offset: 50,
            mobile: false
        });
        wow.init();
        // ---------- Wow Animation ---------- //
    
        // ------- Mesonary ------- //
        var $container = jQuery('.filter-masonry');
        var $optionSets = jQuery('.option-set');
        var $optionLinks = $optionSets.find('a');
        function doIsotopeFilter() {
            if (jQuery().isotope) {
                var isotopeFilter = '';
                $optionLinks.each(function() {
                    var selector = jQuery(this).attr('data-filter');
                    var link = window.location.href;
                    var firstIndex = link.indexOf('filter=');
                    if (firstIndex > 0) {
                        var id = link.substring(firstIndex + 7, link.length);
                        if ('.' + id == selector) {
                            isotopeFilter = '.' + id;
                        }
                    }
                });
                $container.isotope({
                    itemSelector: '.masonry-grid',
                    filter: isotopeFilter
                });
                $optionLinks.each(function() {
                    var $this = jQuery(this);
                    var selector = $this.attr('data-filter');
                    if (selector == isotopeFilter) {
                        if (!$this.hasClass('selected')) {
                            var $optionSet = $this.parents('.option-set');
                            $optionSet.find('.selected').removeClass('selected');
                            $this.addClass('selected');
                        }
                    }
                });
                $optionLinks.on('click', function() {
                    var $this = jQuery(this);
                    var selector = $this.attr('data-filter');
                    $container.isotope({
                        itemSelector: '.masonry-grid',
                        filter: selector
                    });
                    if (!$this.hasClass('selected')) {
                        var $optionSet = $this.parents('.option-set');
                        $optionSet.find('.selected').removeClass('selected');
                        $this.addClass('selected');
                    }
                    return false;
                });
            }
        }
        var isotopeTimer = window.setTimeout(function() {
            window.clearTimeout(isotopeTimer);
            doIsotopeFilter();
        }, 1000);
        jQuery('.simple-masonry').isotope({
          itemSelector: '.simple-masonry-grid'
        });
        // ------- Mesonary ------- // 
    
        // ------- Auto height function ------- //
        var setElementHeight = function () {
            var width = jQuery(window).width();
            /*if (jQuery('.tc-hero-slider li img') >= height) {*/
            var height = jQuery(window).height();
            jQuery('.fullscreen').css('height', (height));
            }
        //       else {
        //            jQuery('.autoheight').css('min-height', (800));
        //        }
        //};
        jQuery(window).on("resize", function () {
            setElementHeight();
        }).resize();
        // ------- Auto height function ------- //
    
        // ------- Prety Photo ------- //
        jQuery("a[data-rel]").each(function () {
            jQuery(this).attr("rel", jQuery(this).data("rel"));
        });
        jQuery("a[data-rel^='prettyPhoto']").prettyPhoto({
            animation_speed: 'normal',
            theme: 'dark_square',
            slideshow: 3000,
            autoplay_slideshow: false,
            social_tools: false
        });
        // ------- Prety Photo ------- //
    
        // ------- PrettyPhoto Video Popup ------- //
        jQuery("a[rel^='prettyPhoto']").prettyPhoto();
        // ------- PrettyPhoto Video Popup ------- //
    
    });