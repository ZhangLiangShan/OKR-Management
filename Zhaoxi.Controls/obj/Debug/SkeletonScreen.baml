   M S B A M L   `   `   `  ����J  FZhaoxi.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Zhaoxi.ControlsZhaoxi.Controls  5      $    Zhaoxi.Controls.SkeletonScreen   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:Zhaoxi.Controls     5        � Triggers  5	      :� 5
   
    :� RoutedEvent$ Window.Loaded��6   .[�5      �� .��5      �� .��5      ��  g� Duration$ 0:0:1D�6%   	 �� To$ #F7F9FAX�66    �� 
TargetName$ imgBlock��6C    �� TargetProperty$* $(Background).(SolidColorBrush.Color)�6d    g� RepeatBehavior$ Forever��6�    g� AutoReverse . 6�   W� 5      $ 0:0:1.5D�6&   	 W� To$ 1X�69   $ border1��6@   $( "Background.GradientStops[1].Offset�6`   $ Forever��6�    . 6�   W� 5      $ 0:0:2D�6&   $ 1X�67   $ border2��6>   $( "Background.GradientStops[1].Offset�6^   $ Forever��6�    . 6�   W� 5      $ 0:0:1.3D�6&   $ 1X�69   $ border3��6@   $( "Background.GradientStops[1].Offset�6`   $ Forever��6�    . 6�   5      5      5      .��5      � $	��5,5q�6   	 � ColumnDefinitions	 5   
   �� 5      $	��160 �6   �� 5      .+�5   
   �� -   5        $��imgBlock��5   L   ���#DDD$��80��6#   $��65��6.   
 �� CornerRadius$
 10��6:   6L   �� 5   
   $��1��6   $��Center=�6%   $��0,0,10,0q�6@   .+�5      �� $
��Left=�6   }�. ._�5      �� -   5        $��border1��5   L   $��16��6   $	��650��6%   $
��Left=�61   $��-200,0q�6[   ��5      �� 5       �� EndPoint$ 1,0.5&�6.    �� 
StartPoint$ 0,0.5&�6?   .��5       �  � Color$
 #DDD��6+    � Offset$ 0Q�68   � 5!      $ #F7F9FA��6+   $ 0Q�6;   � 5"      $
 #DDD��6+   $ 1Q�68   5#      5%      5&      �� 5'      }�. ._�5(      �� -   5        $��border2��5(   5   $��16��6   $��-200,8q�6%   ��5)      �� 5*      $ 1,0.5&�6.   $ 0,0.5&�6?   .��5+      � $
 #DDD��6+   $ 0Q�68   � 5,      $ #F7F9FA��6+   $ 0Q�6;   � 5-      $
 #DDD��6+   $ 1Q�68   5.      50      51      �� 52      $
��Left=�6   }�. ._�53      �� -   5        $��border3��53   L   $��16��6   $	��580��6%   $
��Left=�61   $��-200,0q�6[   ��54      �� 55      $ 1,0.5&�6.   $ 0,0.5&�6?   .��56      � $
 #DDD��6+   $ 0Q�68   � 57      $ #F7F9FA��6+   $ 0Q�6;   � 58      $
 #DDD��6+   $ 1Q�68   59      5;      5<      5=      5>      5?      