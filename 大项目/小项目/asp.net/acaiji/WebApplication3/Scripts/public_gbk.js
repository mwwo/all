//��ҳ��������
$(function(){
	var oli = $('.nav_list ul li');
	oli.hover(function() {
		$(this).children('.slide').stop().slideDown('fast');
	}, function() {
		$(this).children('.slide').slideUp('fast');
	});
});
//��ҳbanner�ֲ�
$(function(){
  if($('#index-bxslider').length>0){
    $('#index-bxslider').bxSlider({
      auto: true,
      pagerCustom: '#bx-pager',
      nextText: ' ',
      prevText: ' ',
      speed: 600,
      pause: 5000
    });
  };
});
//�ƶ��˵���
$(function(){
  var $nav = $("#nav");
  var $navbar=$(".navbar");
  var $navbar_close=$(".navbar_close");
  var lheight =$(window).height()+20;
  $nav.css('height', lheight);
  $navbar.click(function(){
    $nav.animate({left:'0px'});
  });
  $navbar_close.click(function(){
    $nav.animate({left:'-100%'});
  });
});

//�����Ƽ�
$(function(){
    $('#in_host_soft li').hover(function() {
        $(this).find('.solutit2').stop().animate({
            height:'300'
        },400);
    }, function() {
        
        $('#in_host_soft .solutit2').stop().animate({
            height:'0'
        },400);
    });
});

//¥�ݵ���
 $(function(){
    //1.¥��ʲôʱ����ʾ��800px scroll--->scrollTop
    $(window).on('scroll',function(){
        var $scroll=$(this).scrollTop();
        if($scroll>=800){
            $('#loutinav').show();
        }else{
            $('#loutinav').hide();
        }
       // console.log($('.louti').eq(0).html());
        //4.�϶����֣���Ӧ��¥����ʽ����ƥ��
        $('.louti').each(function(index){
            var $loutitop=$('.louti').eq(index).offset().top+400;
            if($loutitop>$scroll){//¥���top���ڹ������ľ���
                $('#loutinav li').removeClass('active');
                $('#loutinav li').eq(index).addClass('active');
                return false;//�ж�ѭ��
            }
        });
    });
    //2.��ȡÿ��¥�ݵ�offset().top,���¥���ö�Ӧ������ģ���ƶ�����Ӧ��λ�� 
    
    var $loutili=$('#loutinav li').not('.last');
    $loutili.on('click',function(){
        $(this).addClass('active').siblings('li').removeClass('active');
        var $loutitop=$('.louti').eq($(this).index()).offset().top - 90;
        //��ȡÿ��¥�ݵ�offsetTopֵ
        $('html,body').animate({//$('html,body')��������body����chrome
            scrollTop:$loutitop
        })
    });
    //3.�ص�����
    $('.last').on('click',function(){
        $('html,body').animate({//$('html,body')��������body����chrome
            scrollTop:0
        })
    });
});
///ѡ�Ч��
$(document).ready(function() {
  jQuery.jqtab = function(tabtit,tab_conbox,shijian) {
    $(tab_conbox).find("li").hide();
    $(tabtit).find("li:first").addClass("thistab").show(); 
    $(tab_conbox).find("li:first").show();
  
    $(tabtit).find("li").bind(shijian,function(){
      $(this).addClass("thistab").siblings("li").removeClass("thistab"); 
      var activeindex = $(tabtit).find("li").index(this);
      $(tab_conbox).children().eq(activeindex).show().siblings().hide();
      return false;
    });
  
  };

  //���÷���
  $.jqtab("#tabs","#tab_conbox","mousedown");
});


// �û����ĵ����������
function timestamp(url){
  var getTimestamp=new Date().getTime(); 
  url=url+"&timestamp="+getTimestamp;
  window.open(url);
}


//�������ͼ
$(function(){
  var img = $('.img');
  var go_left = $('.go_left');
  var go_right = $('.go_right');
  var oul = $('.img_slide ul');
  //var oli = $('.img_slide ul.top li');
  var oli = $('.img_slide ul li');
  var oliWidth = oli.width();
  var num = 0;
  var timeId = null;
  
  go_right.click(function(){
    slide_left();
  });
  
  go_left.click(function(){
    slide_right();
  });
  
  function slide_left(){
    if(num == oli.size() -1){
      num = 0;
    }else{
      num++;
    }
    
    oul.animate({
      left:num*-oliWidth
    },500,function(){
      if(num == 0){
        oul.css('left',0);
      }
    });
  };
  
  function slide_right(){
    if(num <= 0){
      num = 0;
      oul.css('left',0);
    }else{
      num--;
    }
    
    oul.animate({
      left:-num*oliWidth
    },500);
  };
  
  timeId = setInterval(slide_left,3500);
  
  img.hover(function(){
    clearInterval(timeId);
    go_left.show();
    go_right.show()
  },function(){
    timeId = setInterval(slide_left,3500);
    go_left.hide();
    go_right.hide();
  });
  
});


//����չʾ
$(function(){
  var zizhi = $('#zizhi');
  var left = $(window).width()/2 - zizhi.width()/2;
  var top = $(window).height()/2 - zizhi.height()/2;
  var zizhiClick = $('#zizhiClick');
  var close = $('#close');
  var pre = $('.zizhipre');
  var next = $('.zizhinext');
  var wrap = $('.wrap');

  zizhi.css({
    'left':left,
    'top':top
  });

  $(window).resize(function(){
    left = $(window).width()/2 - zizhi.width()/2;
    top = $(window).height()/2 - zizhi.height()/2;
    zizhi.stop().animate({
      'left':left,
      'top':top
    },100);
  });

  zizhiClick.click(function(){
    zizhi.show();
    wrap.show();
  });

  close.click(function(){
    zizhi.hide();
    wrap.hide();
  });

});

//��վ������ͼ
$(function(){
  var oli = $('.logo_tag ul li');
  oli.hover(function() {
    $(this).children('.slide').stop().slideDown('fast');
  }, function() {
    $(this).children('.slide').slideUp('fast');
  });
});

//���ѡ�
$(function(){
  var snn = $('#soft_map_nav ul li');
  snn.mousedown(function(event) {
    $(this).addClass("thistab").siblings("li").removeClass("thistab");
  });
  $('#tab_gongneng').click(function(){
    $('.in_box').css('display','block');
    //$("html,body").animate({scrollTop:$(".in_tuijian2").offset().top},500);
  });
  $('#tab_jiage').click(function(){
    $('.in_box').css('display','none');
    $('#jiage_con').css('display','block'); 
    //$("html,body").animate({scrollTop:$(".in_tuijian2").offset().top},500);
  });
  $('#tab_xiazai').click(function(){
    $('.in_box').css('display','none');
    $('#xiazai_con').css('display','block');
    //$("html,body").animate({scrollTop:$(".in_tuijian2").offset().top},500);
  });
  $('#tab_video').click(function(){
    $('.in_box').css('display','none');
    $('#video_con').css('display','block'); 
    //$("html,body").animate({scrollTop:$(".in_tuijian2").offset().top},500);
  });
  $('#tab_shengji').click(function(){
    $('.in_box').css('display','none');
    $('#shengji_con').css('display','block');
    //$("html,body").animate({scrollTop:$(".in_tuijian2").offset().top},500);
  });
  $('#tab_pingjia').click(function(){
    $('.in_box').css('display','none');
    $('#pingjia_con').css('display','');
    //$("html,body").animate({scrollTop:$(".in_tuijian2").offset().top},500);
  });
  $('#tab_wenti').click(function(){
    $('.in_box').css('display','none');
    $('#wenti_con').css('display','');  
    //$("html,body").animate({scrollTop:$(".in_tuijian2").offset().top},500);
  });
});
//��������
$(function(){
  var pop_window = $('.pop_window');
  var pop_window_down = $('.pop_window_down');
  var pop_window_qq = $('.pop_window_qq');
  var pop_window_box = $('.pop_window_box');
  var pop = $('.down_pop');
  var qq_pop = $('.qq_pop');
  var box_pop =$('.box_pop');
  var shade = $('.shade');
  var close = $('.pop_window h2 span');
  var step = $('.buy_step_02');
  var step_pop = $('.pop_window_step');
  
  var left = ($(window).width() - pop_window.outerWidth())/2;
  var top = ($(window).height() - pop_window.outerHeight())/2;
  
  pop_window.css({
    'left' : left,
    'top' : top
  });
  
  $(window).resize(function(){
    left = ($(window).width() - pop_window.outerWidth())/2;
    top = ($(window).height() - pop_window.outerHeight())/2;
    
    pop_window.stop().animate({
      'left':left,
      'top':top
    },500);
  });
  
  pop.click(function(){
    pop_window_down.show();
    shade.show();
  });
  
  qq_pop.click(function(){
    pop_window_qq.show();
    shade.show();
  });
  
  box_pop.click(function(){
    pop_window_box.show();
    shade.show();
  });
  
  pop.click(function(){
    pop_window_down.show();
    shade.show();
  });
  
  step.click(function(){
    step_pop.show();
    shade.show();
  });
  
  close.click(function(){
    pop_window.hide();
    shade.hide();
    step_pop.hide();
  });
  
  shade.click(function(e){
    pop_window.hide();
    shade.hide();
    step_pop.hide();
    e.stopPropagation();
  });
  
});

// ���ܵ���ϵͳ������
$(function(){
  $('.ai4').hover(function() {
     $('.ai4-2').css('z-index', '9');
     $(this).addClass('active').siblings().removeClass('active');
     if ($(this).hasClass("ai4-4")) {
        $('.ai4-2').css('z-index', '7');
     };
  }, function() {
    /* Stuff to do when the mouse leaves the element */
  });

  // �ƶ���
  var dw = $(window).width();
  if (dw<=640) {
    $('.ai4').addClass('active');
  };
});

// ���ܵ������������Ƶ
// $(function(){
//   var audio = $('audio');
//   $('.au').click(function(){ 
//       var thisIndex = $(this).parents('.ai5-box').index() + 2;
//       $('.ai-ban-btn').css('background-image', 'url(http://res.100public.com/sbfb/aisounds2/banner-btn.png)');
//       if (audio[thisIndex].paused) {
//         $(this).html('��ͣ����').parent('.ai5-box').siblings().children().html('�������');
//         for (var i = 0; i < audio.length; i++) {
//           if ( thisIndex != i ) {
//             audio[i].pause();
//           }else{
//             audio[i].play();
//           }
//         }
//       }else{
//         audioStop();
//      };
//   });
//   $('.ai-ban-btn').click(function() {
//     var topaudio = document.getElementById('topaudio');
//     if (topaudio.paused) {
//        audioStop();
//        topaudio.play();
//        $('.ai-ban-btn').css('background-image', 'url(http://res.100public.com/sbfb/aisounds2/ai-stop.png)');
//     } else{
//       topaudio.pause();
//       $('.ai-ban-btn').css('background-image', 'url(http://res.100public.com/sbfb/aisounds2/banner-btn.png)');
//     };
//   });
//  function audioStop(){
//     $('.au').html('�������');
//     for (var i = 0; i < audio.length; i++) {
//       audio[i].pause();
//     }
//  }
// });

  $(function(){
    $( 'audio' ).audioPlayer({
        classPrefix: 'audioplayer',
        strPlay: '�������',
        strPause: '��ͣ����',
        strVolume: 'Volume'
    });

  });

  function allpause(){
    if (!!window.ActiveXObject || "ActiveXObject" in window){ 
      var audio = $('embed');
    }else{
      var audio = $('audio');
    }
    for (var i = 0; i < audio.length; i++) {
           audio[i].pause();
    }
    $('.audioplayer-playpause').find( 'a' ).html('�������');
  } 



