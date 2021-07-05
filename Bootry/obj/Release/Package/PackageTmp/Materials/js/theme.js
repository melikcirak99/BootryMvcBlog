
$(function(){
    
  var maincarousel = function(){
      $(".owl-carousel").owlCarousel({
      items:4, // Ekranda kaç nesnenin görüneceği
      autoplay:false,
      loop:false,
      responsive:{
          0:{
              items:1
          },
          600:{
              items:1
          },
          1000:{
              items:4
          }
      }

  });
  }

    var BestOfTheWeek = function(){
      $(".owl-carousel").owlCarousel({
      items:4, // Ekranda kaç nesnenin görüneceği
      autoplay:false,
      loop:false,
      responsive:{
          0:{
              items:1
          },
          600:{
              items:1
          },
          1000:{
              items:4
          }
      }

  });
  }
    
  var StickyMenu = function(){
    ///////////////// fixed menu on scroll for desktop
    if ($(window).width() > 992) {
      $(window).scroll(function(){  
         if ($(this).scrollTop() > 150) {
            $('#header').addClass("fixed-top");
          }
          if ($(this).scrollTop() < 1) {
              $('#header').removeClass("fixed-top");
          }
      });
    } // end if
  }

 
  var BackToTop = function(){
    var btn = $('#button');
    $(window).scroll(function() {
      if ($(window).scrollTop() > 300) {
        btn.addClass('show');
      } else {
        btn.removeClass('show');
      }
    });

    btn.on('click', function(e) {
      e.preventDefault();
      $('html, body').animate({scrollTop:0}, '300');
    });
  }
    

  var bestOfTheWeek = function() {  
    var botwCarousel = $(".owl-carousel").owlCarousel({
      items: 4,
      margin: 20,
      nav: false,
      dots: false,
      responsive: {
        1024: {
          items: 5
        },
        768: {
          items: 2
        },
        0: {
          items: 1
        }
      }
    });

    $("#best-of-the-week-nav .next").click(function(){
      botwCarousel.trigger('next.owl.carousel');
    });

    $("#best-of-the-week-nav .prev").click(function(){
      botwCarousel.trigger('prev.owl.carousel');
    });
  }


  var verticalSlider = function () {
    $(".vertical-slider").each(function(ii){
      var $this = $(this), 
          $item = $this.find($this.data("item")),
          $item_height = 0,
          $item_max = $this.data("max"),
          $nav = $($this.data("nav"));

      $this.attr("data-current", 1);

      $item.each(function(i){
        i++;
        $(this).attr("data-list", i);
        if(i > $item_max) {
          return;
        }
        $item_height += ($(this).outerHeight() + 15);
      });

      $this.css({
        overflow: 'hidden'
      });
      $item.wrapAll($("<div/>", {
        id: 'vs_inner_'+ii
      }))

      function vs_next() {
        var $current = $this.attr("data-current"),
            $next = $current;

        var $item_move = $this.find("#vs_inner_"+ii+' '+$this.data("item")+"[data-list="+$next+"]");

        $item_move.fadeOut(function(){
          var $clone = $item_move.clone().fadeIn();
          $item_move.remove();
          $this.find("#vs_inner_"+ii).append($clone);
        });

        $next = parseInt($current) + 1;
        if($next > $item.length) {
          $next = 1;
        }
        $this.attr('data-current', $next);
      }

      function vs_prev() {
        var $current = $this.attr("data-current"),
            $next = $current;

        $next = parseInt($current) - 1;
        if($next < 1) {
          $next = $item.length;
        }
        $this.attr('data-current', $next);

        var $item_move = $this.find("#vs_inner_"+ii+' '+$this.data("item")+"[data-list="+$next+"]");
        var $clone = $item_move.clone().css('display','none');
        $item_move.remove();
        $this.find("#vs_inner_"+ii).prepend($clone.fadeIn());
      }

      $nav.find(".prev").click(function(){
        vs_prev();
      });
      $nav.find(".next").click(function(){
        vs_next();
      });
      setInterval(function(){
        vs_next();
      },10000);
    });   
  }
    


  maincarousel();
 
  //StickyMenu();

  verticalSlider();

  bestOfTheWeek();

  BackToTop();
});
